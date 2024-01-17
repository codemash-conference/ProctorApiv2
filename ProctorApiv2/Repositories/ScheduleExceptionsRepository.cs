using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ProctorApiv2.Models;

namespace ProctorApiv2.Repositories
{
    public class ScheduleExceptionsRepository : BaseSqlRepository
    {
        private readonly UsersRepository _userRepository;

        public ScheduleExceptionsRepository()
        {
            _userRepository = new UsersRepository();
        }

        public List<ScheduleException> GetAll()
        {
            var spName = "ScheduleExceptionGetAll";
            List<ScheduleException> exceptions = GetFromSQL<ScheduleException>(_connStr, spName, AutoConvert<ScheduleException>);
            AttachUserInfo(exceptions);

            return exceptions;
        }

        private void AttachUserInfo(List<ScheduleException> exceptions)
        {
            var users = _userRepository.GetUsers();
            exceptions.ForEach(se => se.User = users.FirstOrDefault(u => u.Id == se.User_Id));

        }

        public int CreateException(ScheduleException scheduleException)
        {
            var spName = "ScheduleExceptionUpsert";

            int id = ExecuteScalerStatement(_connStr, (conn, cmd) => {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = spName;
                if (scheduleException.Id != null && scheduleException.Id != 0)
                {
                    cmd.Parameters.AddWithValue("@Id", scheduleException.Id);
                }
                cmd.Parameters.AddWithValue("@StartTime", scheduleException.StartTime);
                cmd.Parameters.AddWithValue("@EndTime", scheduleException.EndTime);
                cmd.Parameters.AddWithValue("@UserId", scheduleException.User_Id);
            });

            return id;
        }
    }
}