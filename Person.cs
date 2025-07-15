using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Runtime.Remoting.Messaging;
using ContactsDataAccessLayer;

namespace ContactsBusinessLayer
{
    public class clsPersonBusiness
    {
        public int PersonId { get; set; }
        public string NationalID { get; set; }
        public string FullName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Nationality { get; set; }
        public string Photo { get; set; }

        // Business Methods:

        public static List<clsPersonBusiness> GetAllPersons()
        {
            List<clsPersonBusiness> persons = new List<clsPersonBusiness>();
            var dataList = clsPersonData.GetAllPersonData();
            foreach (var item in dataList)
            {
                persons.Add(MapToBusiness(item));
            }
            return persons;
        }

        public static clsPersonBusiness GetPersonById(int personId)
        {
            var dataPerson = clsPersonData.GetPersonById(personId);
            return dataPerson != null ? MapToBusiness(dataPerson) : null;
        }

        //public static clsPersonBusiness GetPersonByNationalID(string nationalId)
        //{
        //    var dataPerson = clsPersonData.GetPersonByNationalID(nationalId);
        //    return dataPerson != null ? MapToBusiness(dataPerson) : null;
        //}

        //get persons by NationalID

        public static List<clsPersonBusiness> GetPersonByNationalID(string nationalId)
        {
            List<clsPersonBusiness> persons = new List<clsPersonBusiness>();
            var dataList = clsPersonData.GetPersonByNationalID(nationalId);
            foreach (var item in dataList)
            {
                persons.Add(MapToBusiness(item));
            }
            return persons;
        }

        public static List<clsPersonBusiness> GetPersonsByNationality(string nationality)
        {
            List<clsPersonBusiness> persons = new List<clsPersonBusiness>();
            var dataList = clsPersonData.GetPersonByNationality(nationality);
            foreach (var item in dataList)
            {
                persons.Add(MapToBusiness(item));
            }
            return persons;
        }


        public bool Save()
        {
            try
            {
                clsPerson data = MapToData(this);
                if (clsPersonData.GetPersonById(this.PersonId) == null)
                {
                    return clsPersonData.AddPerson(data); // ترجع true إذا نجح
                }
                else
                {
                    return clsPersonData.UpdatePerson(data); // ترجع true إذا نجح
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error saving person: " + ex.Message);
                return false;
            }
        }

        //  update person 

        public static bool UpdatePerson(clsPersonBusiness Person)
        {
            try
            {
                return Person.Save();


            }
            catch (Exception ex)
            {
                // Log the exception or handle it as needed
                Console.WriteLine("Error adding person: " + ex.Message);
                return false;
            }


        }





        public static bool Delete(int PersonId )
        {
            try
            {
                return clsPersonData.DeletePerson(PersonId);

            }
            catch (Exception ex)
            {
                // Log the exception or handle it as needed
                Console.WriteLine("Error deleting person: " + ex.Message);
                return false;
            }

        }

        // Helper Methods:
        private static clsPersonBusiness MapToBusiness(clsPerson data)
        {
            return new clsPersonBusiness
            {
                PersonId = data.PersonId,
                NationalID = data.NationalID,
                FullName = data.FullName,
                DateOfBirth = data.DateOfBirth,
                Address = data.Address,
                Phone = data.PhoneNumber,
                Email = data.Email,
                Nationality = data.Nationality,
                Photo = data.Photo          
            };
        }

        private static clsPerson MapToData(clsPersonBusiness business)
        {
            return new clsPerson
            {
                PersonId = business.PersonId,
                NationalID = business.NationalID,
                FullName = business.FullName,
                DateOfBirth = business.DateOfBirth,
                Address = business.Address,
                PhoneNumber = business.Phone,
                Email = business.Email,
                Nationality = business.Nationality,
                Photo = business.Photo
            };
        }


        // add new person
        public static bool AddPerson(clsPersonBusiness person)
        {
            try
            {
                return person.Save();
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as needed
                Console.WriteLine("Error adding person: " + ex.Message);
                return false;
            }

        }
    }
}
