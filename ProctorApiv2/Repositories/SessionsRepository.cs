using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using ProctorApiv2.Models;
using ProctorApiv2.ViewModels;

namespace ProctorApiv2.Repositories
{
    public class SessionsRepository : BaseSqlRepository
    {
        private string _speakerFeed;
        private string _sessionFeed;
        private readonly SessionTypesRepository _sessionTypesRepository;
        private readonly SpeakersRepository _speakersRepository;
        private readonly TagsRepository _tagsRepository;
        private readonly RoomsRepository _roomsRepository;
        private readonly UsersRepository _userRepository;
        private readonly ProctorCheckInsRepository _proctorCheckInsRepository;


        public SessionsRepository()
        {
            _speakerFeed = ConfigurationManager.AppSettings["SpeakerFeed"];
            _sessionFeed = ConfigurationManager.AppSettings["SessionFeed"];

            _sessionTypesRepository = new SessionTypesRepository();
            _speakersRepository = new SpeakersRepository();
            _roomsRepository = new RoomsRepository();
            _tagsRepository = new TagsRepository();
            _userRepository = new UsersRepository();
            _proctorCheckInsRepository = new ProctorCheckInsRepository();
        }

       

        public void ImportFromFeed()
        {
            ImportSpeakers();
            ImportSessions();
        }

        private void ImportSpeakers()
        {
            using (var webClient = new System.Net.WebClient())
            {
                var json = webClient.DownloadString(_speakerFeed);
                
                List<ViewModels.SessionizeSpeaker.Speaker> speakerImport = JsonConvert.DeserializeObject<List<ViewModels.SessionizeSpeaker.Speaker>>(json);

                foreach (ViewModels.SessionizeSpeaker.Speaker speaker in speakerImport)
                {
                    var mappedSpeaker = MapSpeaker(speaker);
                    UpsertSpeaker(mappedSpeaker);
                }
            }
        }

        private SpeakerImport MapSpeaker(ViewModels.SessionizeSpeaker.Speaker speaker)
        {
            var mappedSpeaker = new SpeakerImport();
            var blog = speaker.links.FirstOrDefault(x => x.LinkType == "Blog");
            var linkedIn = speaker.links.FirstOrDefault(x => x.LinkType == "LinkedIn");
            var twitter = speaker.links.FirstOrDefault(x => x.LinkType == "Twitter");

            mappedSpeaker.Biography = speaker.bio;
            mappedSpeaker.BlogUrl = blog == null ? "" : blog.Url;
            mappedSpeaker.FirstName = speaker.firstName;
            mappedSpeaker.GravatarUrl = speaker.profilePicture;
            mappedSpeaker.Id = speaker.id;
            mappedSpeaker.LastName = speaker.lastName;
            mappedSpeaker.LinkedInProfile = linkedIn == null ? "" : linkedIn.Url;
            mappedSpeaker.TwitterLink = twitter == null ? "" : twitter.Url;            

            return mappedSpeaker;            
        }

        public Session getSessionById(int sessionId)
        {
            var spName = "SessionGetById";

            List<SqlParameter> parms = new List<SqlParameter>();
            parms.Add(StringParameter("sessionId", sessionId.ToString()));

            var session =  GetFromSQLSingle<Session>(_connStr, spName, AutoConvert<Session>, parms);

            session.SessionType = _sessionTypesRepository.GetBySessionId(session.SessionType_Id);
            session.Speakers = _speakersRepository.GetBySessionId(session.Id);
            session.Rooms = _roomsRepository.GetBySessionId(session.Id);
            session.Tags = _tagsRepository.GetBySessionId(session.Id);
            session.Assignees = _userRepository.GetBySessionId(session.Id);
            session.ProctorCheckIns = _proctorCheckInsRepository.getBySessionId(session.Id);

            return session;
        }

        public List<Session> getSessions()
        {
            var spName = "SessionGetAll";
            List<Session> sessions = GetFromSQL<Session>(_connStr, spName, AutoConvert<Session>);
            AttachSessionInfo(sessions);

            return sessions;

        }

        private void AttachSessionInfo(List<Session> sessions)
        {
            using (SqlConnection conn = new SqlConnection(_connStr))
            {
                using (SqlCommand cmd = new SqlCommand("SessionGetAllInfo", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    conn.Open();

                    var dr = cmd.ExecuteReader();

                    //Session Types
                    while (dr.Read())
                    {
                        var sessionType = AutoConvert<SessionType>(dr);

                        var sessionWithCurrentSessionType = sessions.FindAll(s => s.SessionType_Id == sessionType.Id);

                        sessionWithCurrentSessionType.ForEach(s => s.SessionType = sessionType);
                    }

                    //Rooms
                    dr.NextResult();
                    while (dr.Read())
                    {
                        var sessionId = IntegerValue(dr["Session_Id"]);
                        var session = sessions.FirstOrDefault(s => s.Id == sessionId);
                        if (session != null)
                        {
                            session.Rooms.Add(AutoConvert<Room>(dr));
                        }
                    }

                    //Speakers
                    dr.NextResult();
                    while (dr.Read())
                    {
                        var sessionId = IntegerValue(dr["Session_Id"]);
                        var session = sessions.FirstOrDefault(s => s.Id == sessionId);
                        if (session != null)
                        {
                            session.Speakers.Add(AutoConvert<Models.Speaker>(dr));
                        }
                    }

                    //Tags
                    dr.NextResult();
                    while (dr.Read())
                    {
                        var sessionId = IntegerValue(dr["Session_Id"]);
                        var session = sessions.FirstOrDefault(s => s.Id == sessionId);
                        if (session != null)
                        {
                            session.Tags.Add(AutoConvert<Models.Tag>(dr));
                        }
                    }

                    //UserCheckIns
                    dr.NextResult();
                    while (dr.Read())
                    {
                        var sessionId = IntegerValue(dr["SessionId"]);
                        var session = sessions.FirstOrDefault(s => s.Id == sessionId);
                        if (session != null)
                        {
                            session.ProctorCheckIns.Add(AutoConvert<Models.UserCheckIn>(dr));
                        }
                    }

                    //Assignees
                    dr.NextResult();
                    while (dr.Read())
                    {
                        var sessionId = IntegerValue(dr["Session_Id"]);
                        var session = sessions.FirstOrDefault(s => s.Id == sessionId);
                        if (session != null)
                        {
                            session.Assignees.Add(AutoConvert<Models.User>(dr));
                        }
                    }

                }
            }
        }

        private void UpsertSpeaker(SpeakerImport speaker)
        {
            var spName = "SpeakerUpsert";

            ExecuteStatement(_connStr, (conn, cmd) =>
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = spName;

                cmd.Parameters.AddWithValue("@Biography", speaker.Biography);
                cmd.Parameters.AddWithValue("@BlogUrl", speaker.BlogUrl);
                cmd.Parameters.AddWithValue("@FirstName", speaker.FirstName);
                cmd.Parameters.AddWithValue("@GitHubLink", speaker.GitHubLink);
                cmd.Parameters.AddWithValue("@GravatarUrl", speaker.GravatarUrl);
                cmd.Parameters.AddWithValue("@Id", speaker.Id);
                cmd.Parameters.AddWithValue("@LastName", speaker.LastName);
                cmd.Parameters.AddWithValue("@LinkedInProfile", speaker.LinkedInProfile);
                cmd.Parameters.AddWithValue("@TwitterLink", speaker.TwitterLink);
            });
        }

        internal void Update(int id, Session session)
        {
            var spName = "SessionUpsert";

            string speakersStr = "";
            string roomsStr = "";
            string tagsStr = "";
            if (session.Speakers != null)
                session.Speakers.ForEach(s => speakersStr += ',' + s.Id);
            if (session.Tags != null)
                session.Tags.ForEach(t => tagsStr += ',' + t.Name);
            if (session.Rooms != null)
                session.Rooms.ForEach(r => roomsStr += ',' + r.Name);

            if (string.IsNullOrEmpty(speakersStr)) { speakersStr = ","; }
            if (string.IsNullOrEmpty(tagsStr)) { tagsStr = ","; }
            if (string.IsNullOrEmpty(roomsStr)) { roomsStr = ","; }

            ExecuteStatement(_connStr, (conn, cmd) =>
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = spName;

                cmd.Parameters.AddWithValue("@Abstract", session.Abstract);
                cmd.Parameters.AddWithValue("@Category", session.Category);
                cmd.Parameters.AddWithValue("@Id", session.Id);
                cmd.Parameters.AddWithValue("@SessionEndTime", session.SessionEndTime);
                cmd.Parameters.AddWithValue("@SessionStartTime", session.SessionStartTime);
                //cmd.Parameters.AddWithValue("@SessionTime", session.SessionTime);
                cmd.Parameters.AddWithValue("@SessionType", session.SessionType.Name);
                cmd.Parameters.AddWithValue("@Title", session.Title);
                cmd.Parameters.AddWithValue("@Rooms", roomsStr.Substring(1));
                cmd.Parameters.AddWithValue("@Tags", tagsStr.Substring(1));
                cmd.Parameters.AddWithValue("@Speakers", speakersStr.Substring(1));
                cmd.Parameters.AddWithValue("@VolunteersRequired", session.VolunteersRequired);
                cmd.Parameters.AddWithValue("@Attendees10", session.Attendees10);
                cmd.Parameters.AddWithValue("@Attendees50", session.Attendees50);
                cmd.Parameters.AddWithValue("@Notes", session.Notes);
                if (session.ActualSessionStartTime != null)
                    cmd.Parameters.AddWithValue("@ActualSessionStartTime", session.ActualSessionStartTime);

                if (session.ActualSessionEndTime != null)
                    cmd.Parameters.AddWithValue("@ActualSessionEndTime", session.ActualSessionEndTime);


            });

            if(session.ProctorCheckIns != null && session.ProctorCheckIns.Count > 0)
            {
                foreach(var proctorCheckIn in session.ProctorCheckIns)
                {
                    _proctorCheckInsRepository.UpsertProctorCheckIn(proctorCheckIn);
                }
            }

            
            
        }

        private void ImportSessions()
        {
            using (var webClient = new System.Net.WebClient())
            {
                var json = webClient.DownloadString(_sessionFeed);

                List<ViewModels.SessionizeSession.RootObject> sessionImport = JsonConvert.DeserializeObject<List<ViewModels.SessionizeSession.RootObject>>(json);
                
                foreach (ViewModels.SessionizeSession.Session session in sessionImport.FirstOrDefault().sessions)
                {
                    //session.FeedSessionId = session.Id;
                    //session.Id = 0;
                    var mappedSession = MapSession(session);
                    UpsertSession(mappedSession);
                }
            }
        }

        private SessionImport MapSession(ViewModels.SessionizeSession.Session session)
        {
            var mappedSession = new SessionImport();
            mappedSession.Speakers = new List<ViewModels.Speaker>();
            mappedSession.Tags = new List<string>();

            var sessionFormat = session.categories.FirstOrDefault(x => x.name == "Session format");
            var sessionTrack = session.categories.FirstOrDefault(x => x.name == "Track");
            var sessionTags = session.categories.FirstOrDefault(x => x.name == "Tags");

            mappedSession.Abstract = session.description;
            mappedSession.Category = sessionTrack == null ? "" : sessionTrack.categoryItems[0].Name;
            mappedSession.FeedSessionId = session.id;
            mappedSession.Room = session.room;
            mappedSession.SessionEndTime = session.endsAt;
            mappedSession.SessionStartTime = session.startsAt;
            mappedSession.SessionTime = session.startsAt;
            mappedSession.SessionType = sessionFormat == null ? "" : sessionFormat.categoryItems[0].Name;            
            foreach (var speaker in session.speakers)
            {
                mappedSession.Speakers.Add(new ViewModels.Speaker() {
                    Id = speaker.id
                });
            }

            if(sessionTags != null && sessionTags.categoryItems.Count > 0)
            {
                foreach(var tag in sessionTags.categoryItems)
                {
                    mappedSession.Tags.Add(tag.Name);
                }
            }

            mappedSession.Title = session.title;

            return mappedSession;
        }

        public void UpsertSession(SessionImport session)
        {
            var spName = "SessionUpsert";

            string speakersStr = "";
            
            string tagsStr = "";
            session.Speakers.ForEach(s => speakersStr += ',' + s.Id);
            session.Tags.ForEach(t => tagsStr += ',' + t);
            

            if (string.IsNullOrEmpty(speakersStr)) { speakersStr = ","; }
            if (string.IsNullOrEmpty(tagsStr)) { tagsStr = ","; }
            

            ExecuteStatement(_connStr, (conn, cmd) =>
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = spName;

                cmd.Parameters.AddWithValue("@Abstract", session.Abstract);
                cmd.Parameters.AddWithValue("@Category", session.Category);
                cmd.Parameters.AddWithValue("@Id", session.Id);
                cmd.Parameters.AddWithValue("@FeedSessionId", session.FeedSessionId);
                cmd.Parameters.AddWithValue("@SessionEndTime", session.SessionEndTime);
                cmd.Parameters.AddWithValue("@SessionStartTime", session.SessionStartTime);
                cmd.Parameters.AddWithValue("@SessionTime", session.SessionTime);
                cmd.Parameters.AddWithValue("@SessionType", session.SessionType);
                cmd.Parameters.AddWithValue("@Title", session.Title);
                cmd.Parameters.AddWithValue("@Rooms", session.Room);
                cmd.Parameters.AddWithValue("@Tags", tagsStr.Substring(1));
                cmd.Parameters.AddWithValue("@Speakers", speakersStr.Substring(1));
            });

        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public int Create(Session session)
        {
            var spName = "SessionUpsert";

            string speakersStr = "";
            string roomsStr = "";
            string tagsStr = "";
            if (session.Speakers != null)
                session.Speakers.ForEach(s => speakersStr += ',' + s.Id);
            if (session.Tags != null)
                session.Tags.ForEach(t => tagsStr += ',' + t.Name);
            if (session.Rooms != null)
                session.Rooms.ForEach(r => roomsStr += ',' + r.Name);

            if (string.IsNullOrEmpty(speakersStr)) { speakersStr = ","; }
            if (string.IsNullOrEmpty(tagsStr)) { tagsStr = ","; }
            if (string.IsNullOrEmpty(roomsStr)) { roomsStr = ","; }

            if (session.VolunteersRequired == 0)
            {
                session.VolunteersRequired = 1;
            }

            var id = ExecuteScalerStatement(_connStr, (conn, cmd) =>
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = spName;

                cmd.Parameters.AddWithValue("@Abstract", session.Abstract);
                cmd.Parameters.AddWithValue("@Category", session.Category);
                cmd.Parameters.AddWithValue("@Id", session.Id);
                cmd.Parameters.AddWithValue("@SessionEndTime", session.SessionEndTime);
                cmd.Parameters.AddWithValue("@SessionStartTime", session.SessionStartTime);
                //cmd.Parameters.AddWithValue("@SessionTime", session.SessionTime);
                cmd.Parameters.AddWithValue("@SessionType", session.SessionType.Name);
                cmd.Parameters.AddWithValue("@Title", session.Title);
                cmd.Parameters.AddWithValue("@Rooms", roomsStr.Substring(1));
                cmd.Parameters.AddWithValue("@Tags", tagsStr.Substring(1));
                cmd.Parameters.AddWithValue("@Speakers", speakersStr.Substring(1));
                cmd.Parameters.AddWithValue("@VolunteersRequired", session.VolunteersRequired);

            });

            return id;

        }

        public void AutoAssign()
        {
            var spName = "AutoAssignUsersToSessions";

            ExecuteStatement(_connStr, (conn, cmd) =>
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = spName;
            });
        }

        public List<User> GetSessionsPerUser()
        {
            var allUsers = _userRepository.GetUsers();
            List<User> users = new List<User>();
            allUsers.ForEach(user => users.Add(GetSessionsForUser(user.Id)));
            return users;            
        }

        public User GetSessionsForUser(string userId)
        {
            var user = _userRepository.GetUserById(userId);

            var spName = "SessionGetAllForUser";

            List<SqlParameter> parms = new List<SqlParameter>();
            parms.Add(StringParameter("UserId", userId));
            List<Session> sessions = GetFromSQL<Session>(_connStr, spName, AutoConvert<Session>, parms);
            AttachSessionInfo(sessions);

            user.Sessions = sessions;

            return user;
        }

        public void AddUserToSession(string userId, int sessionId)
        {
            var spName = "SessionAssignUser";

            ExecuteStatement(_connStr, (conn, cmd) =>
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = spName;

                cmd.Parameters.AddWithValue("@userId", userId);
                cmd.Parameters.AddWithValue("@SessionId", sessionId);
            });
        }

        public void RemoveUserFromSession(string userId, int sessionId)
        {
            var spName = "SessionUnassignUser";

            ExecuteStatement(_connStr, (conn, cmd) =>
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = spName;

                cmd.Parameters.AddWithValue("@userId", userId);
                cmd.Parameters.AddWithValue("@SessionId", sessionId);
            });
        }

        public List<SessionResult> GetSessionResults()
        {
            throw new NotImplementedException();
        }
    }
}