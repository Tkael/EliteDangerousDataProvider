﻿using EddiInaraResponder;
using EddiInaraService;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitTests;

namespace Apitests
{
    [TestClass]
    public class InaraTests : TestBase
    {
        [TestInitialize]
        public void start()
        {
            MakeSafe();
        }

        [TestMethod]
        public void TestSendEventBatch()
        {
            List<InaraAPIEvent> inaraAPIEvents = new List<InaraAPIEvent>()
            {
                { new InaraAPIEvent(DateTime.UtcNow, "getCommanderProfile", new Dictionary<string, object>() { { "searchName", "No such name" } })},
                { new InaraAPIEvent(DateTime.UtcNow, "getCommanderProfile", new Dictionary<string, object>() { { "searchName", "Artie" } })}
            };
            List<InaraResponse> responses = InaraService.Instance.SendEventBatch(ref inaraAPIEvents, true);

            // Check that appropriate response IDs were assigned to each API event
            Assert.AreEqual(0, inaraAPIEvents[0].eventCustomID);
            Assert.AreEqual(1, inaraAPIEvents[1].eventCustomID);

            // Check that only valid responses were returned and that the response indices are correct
            Assert.AreEqual(1, responses.Count);
            Assert.AreEqual(1, responses[0].eventCustomID);
        }

        [TestMethod]
        public void TestGetCmdrProfiles()
        {
            List<InaraCmdr> inaraCmdrs = InaraService.Instance
                .GetCommanderProfiles(new string[] { "No such name", "Artie" }, true);
            Assert.AreEqual(1, inaraCmdrs.Count);
            Assert.AreEqual("Artie", inaraCmdrs[0].userName);
            Assert.AreEqual(1, inaraCmdrs[0].userID);
            Assert.AreEqual("Artie", inaraCmdrs[0].commanderName);
            Assert.AreEqual(@"https://inara.cz/cmdr/1/", inaraCmdrs[0].inaraURL);
        }
    }
}