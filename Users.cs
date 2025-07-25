using System;
using System.Collections.Generic;
using ContactsDataAccessLayer;

namespace ContactsBusinessLayer
{
    public class clsUsers
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Role { get; set; }
        public string Description { get; set; }
        public string UserPassword { get; set; }

        public static List<clsUsers> GetAllUsers()
        {
            List<clsUsers> usersList = new List<clsUsers>();
            var dataUsers = clsUsersData.GetAllUsersData();

            foreach (var dataUser in dataUsers)
            {
                usersList.Add(MapDataToBusiness(dataUser));
            }

            return usersList;
        }

        public static clsUsers GetUserByID(int userId)
        {
            var dataUser = clsUsersData.GetUserByUserID(userId);
            return dataUser != null ? MapDataToBusiness(dataUser) : null;
        }

        public static List<clsUsers> GetUsersByPersonID(int personID)
        {
            List<clsUsers> usersList = new List<clsUsers>();
            var dataUsers = clsUsersData.GetUsersByPersonID(personID);

            foreach (var dataUser in dataUsers)
            {
                usersList.Add(MapDataToBusiness(dataUser));
            }

            return usersList;
        }

        public static List<clsUsers> GetUsersByUserName(string userName)
        {
            List<clsUsers> usersList = new List<clsUsers>();
            var dataUsers = clsUsersData.GetUsersByUserName(userName);

            foreach (var dataUser in dataUsers)
            {
                usersList.Add(MapDataToBusiness(dataUser));
            }

            return usersList;
        }

        public static List<clsUsers> GetUsersByRole(string role)
        {
            List<clsUsers> usersList = new List<clsUsers>();
            var dataUsers = clsUsersData.GetUsersByRole(role);

            foreach (var dataUser in dataUsers)
            {
                usersList.Add(MapDataToBusiness(dataUser));
            }

            return usersList;
        }



        public bool Save(clsUsers clsUsers)
        {
            var dataUser = MapBusinessToData(this);
            if (clsUsersData.GetUserByUserID(this.UserId) == null)
            {
                // Add
                return clsUsersData.AddUserData(dataUser);
            }
            else
            {
                // Update
                return clsUsersData.UpdateUserData(dataUser);
            }
        }

        // Fix for CS0026: Replace 'this' with 'clsUsers' parameter in static Update method
        public static bool Update(clsUsers clsUsers)
        {
            var dataUser = MapBusinessToData(clsUsers);
            return clsUsersData.UpdateUserData(dataUser);
        }


        public bool Delete(int userid)
        {
            return clsUsersData.DeleteUserData(this.UserId);
        }

        public static bool DeleteUser(int userId)
        {
            return clsUsersData.DeleteUserData(userId);
        }


        private static clsUsers MapDataToBusiness(clsUser dataUser)
        {
            return new clsUsers
            {
                UserId = dataUser.UserId,
                UserName = dataUser.UserName,
                UserPassword = dataUser.UserPassword,
                Role = dataUser.Role,
                Description = dataUser.Description
            };
        }

        private static clsUser MapBusinessToData(clsUsers businessUser)
        {
            return new clsUser
            {
                UserId = businessUser.UserId,
                UserName = businessUser.UserName,
                UserPassword = businessUser.UserPassword,
                Role = businessUser.Role,
                Description = businessUser.Description
            };
        }
    }
}
