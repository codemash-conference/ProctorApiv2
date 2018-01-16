using System;
using System.Collections.Generic;
using System.Configuration;
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

        public SessionsRepository()
        {
            _speakerFeed = ConfigurationManager.AppSettings["SpeakerFeed"];
            _sessionFeed = ConfigurationManager.AppSettings["SessionFeed"];
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
                
                List<SpeakerImport> speakerImport = JsonConvert.DeserializeObject<List<SpeakerImport>>(json);

                foreach (SpeakerImport speaker in speakerImport)
                {
                    UpsertSpeaker(speaker);
                }
            }
        }

        public Session getSessionById(int id)
        {
            throw new NotImplementedException();
        }

        public List<Session> getSessions()
        {
            throw new NotImplementedException();
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

        private void ImportSessions()
        {
            using (var webClient = new System.Net.WebClient())
            {
                var json = webClient.DownloadString(_sessionFeed);
                
                List<SessionImport> sessionImport = JsonConvert.DeserializeObject<List<SessionImport>>(json);

                foreach (SessionImport session in sessionImport)
                {
                    UpsertSession(session);
                }

                //var dbSessions = _context.Sessions.ToList();

                //var sessionsToBeDeleted =
                //dbSessions.Where(x => !sessionImport.Any(s => s.Id == x.FeedSessionId)).ToList();

                //foreach (var session in sessionsToBeDeleted)
                //{
                //    _context.Sessions.Remove(session);
                //    _context.SaveChanges();
                //}

            }
        }
        public void UpsertSession(SessionImport session)
        {
            var spName = "SessionUpsert";

            string speakersStr = "";
            string roomsStr = "";
            string tagsStr = "";
            session.Speakers.ForEach(s => speakersStr += ',' + s.Id);
            session.Tags.ForEach(t => tagsStr += ',' + t);
            session.Rooms.ForEach(r => roomsStr += ',' + r);

            ExecuteStatement(_connStr, (conn, cmd) =>
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = spName;

                cmd.Parameters.AddWithValue("@Abstract", session.Abstract);
                cmd.Parameters.AddWithValue("@Category", session.Category);
                cmd.Parameters.AddWithValue("@Id", session.Id);
                cmd.Parameters.AddWithValue("@SessionEndTime", session.SessionEndTime);
                cmd.Parameters.AddWithValue("@SessionStartTime", session.SessionStartTime);
                cmd.Parameters.AddWithValue("@SessionTime", session.SessionTime);
                cmd.Parameters.AddWithValue("@SessionType", session.SessionType);
                cmd.Parameters.AddWithValue("@Title", session.Title);
                cmd.Parameters.AddWithValue("@Rooms", roomsStr.Substring(1));
                cmd.Parameters.AddWithValue("@Tags", tagsStr.Substring(1));
                cmd.Parameters.AddWithValue("@Speakers", speakersStr.Substring(1));
            });

        }
    }
}