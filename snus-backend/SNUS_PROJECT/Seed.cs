using SNUS_PROJECT.Data;
using SNUS_PROJECT.Models;
using System.Collections.Generic;
using System.Data;
using System.Net;

namespace SNUS_PROJECT
{
    public class Seed
    {
        private readonly DataContext _dataContext;
        public Seed(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public void SeedDataContext()
        {
            if (!_dataContext.Users.Any())
            {
                AddUsers();
                _dataContext.SaveChanges();

            }
            if (!_dataContext.AnalogInputs.Any())
            {
                AddAnalogInputTags();
                _dataContext.SaveChanges();

            }
            if (!_dataContext.AnalogOutputs.Any())
            {
                AddAnalogOutputTags();
                _dataContext.SaveChanges();

            }
            if (!_dataContext.DigitalInputs.Any())
            {
                AddDigitalInputTags();
                _dataContext.SaveChanges();

            }
            if (!_dataContext.DigitalOutputs.Any())
            {
                AddDigitalOutputTags();
                _dataContext.SaveChanges();

            }
            if (!_dataContext.Alarms.Any())
            {
                AddAlarms();
                _dataContext.SaveChanges();

            }

        }

        private void AddUsers()
        {
            var users = new List<User>()
            {
                new User("admin", "admin", 0),
                new User("user", "user", 1)
            };

            _dataContext.Users.AddRange(users);


        }

        private void AddAnalogInputTags()
        {
            var analogTags = new List<AnalogInput>()
        {
            new AnalogInput("AIName1", "AIDescription1", "AIIOAddress1", "AIDriver1", 20.0, true, new List<Alarm>(), 0.0, 50.0, "mA"),
            new AnalogInput("AIName2", "AIDescription2", "AIIOAddress2", "AIDriver2", 20.0, true, new List<Alarm>(), 0.0, 30.0, "V"),
            new AnalogInput("AIName3", "AIDescription3", "AIIOAddress3", "AIDriver3", 20.0, true, new List<Alarm>(), 0.0, 80.0, "KW"),
        };
            _dataContext.AnalogInputs.AddRange(analogTags);
        }
        private void AddAnalogOutputTags()
        {
            var analogTags = new List<AnalogOutput>()
        {
            new AnalogOutput("AO1", "Adresa1", "Analogni output1", 0, 0.0, 50.0, "mA"),
            new AnalogOutput("AO2", "Adresa2", "Analogni output2", 0, 0.0, 20.0, "V"),
            new AnalogOutput("AO3", "Adresa3", "Analogni output3", 0, 0.0, 30.0, "KW"),
        };
            _dataContext.AnalogOutputs.AddRange(analogTags);
        }
        private void AddDigitalInputTags()
        {
            var digitalTags = new List<DigitalInput>()
        {
            new DigitalInput("DI1", "Desc1", "IOAddr1", "Driver1", 20.0, true),
            new DigitalInput("DI2", "Desc2", "IOAddr2", "Driver2", 20.0, false),
            new DigitalInput("DI3", "Desc3", "IOAddr3", "Driver3", 20.0, true)
        };
            _dataContext.DigitalInputs.AddRange(digitalTags);
        }
        private void AddDigitalOutputTags()
        {
            var digitalTags = new List<DigitalOutput>()
        {
            new DigitalOutput("DO1", "Digitalni output 1", "DOAddress1", 20.0),
            new DigitalOutput("DO2", "Digitalni output 2", "DOAddress2", 30.0),
            new DigitalOutput("DO3", "Digitalni output 3", "DOAddress3", 50.0)
        };
            _dataContext.DigitalOutputs.AddRange(digitalTags);
        }

        private void AddAlarms()
        {
            AnalogInput analogInput = _dataContext.AnalogInputs.FirstOrDefault();

            var alarms = new List<Alarm>()
        {
            new Alarm(30, "Visoka struja", analogInput, analogInput.Id, DateTime.Now, 1, "High", analogInput.Units, false),
            new Alarm(46, "Previsoka struja", analogInput, analogInput.Id, DateTime.Now, 3, "High", analogInput.Units, false),
            new Alarm(20, "Niska struja",  analogInput, analogInput.Id, DateTime.Now, 2, "High", analogInput.Units, false),
        };

            _dataContext.Alarms.AddRange(alarms);
            _dataContext.SaveChanges();
            Console.WriteLine(_dataContext.Alarms.FirstOrDefault());
            analogInput.Alarms = _dataContext.Alarms.ToList();
            _dataContext.SaveChanges();


        }

    }
    }