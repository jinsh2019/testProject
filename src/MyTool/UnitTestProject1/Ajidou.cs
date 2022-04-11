﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Principal;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.EntityFrameworkCore.ValueGeneration.Internal;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace UnitTestProject1
{
    [TestClass]
    public class Ajidou
    {
        [TestMethod]
        public void GetConvertOracleNumToDateTime()
        {
            long[] nums = { 121151, 100000, 122270 };
            int thisYear = DateTime.Now.Year;
            for (int i = 0; i < nums.Length; i++)
            {
                int k = 0;
                while (nums[i] + 1900000 - (thisYear - k) * 1000 < 0)
                {
                    k++;
                }
                while (nums[i] + 1900000 - (thisYear - k) * 1000 > 1000)
                {
                    k--;
                }
                Console.WriteLine(nums[i].ToString() + " " + new DateTime(thisYear - k, 1, 1).AddDays(nums[i] + 1900000 - (thisYear - k) * 1000).ToString("yyyy-MM-dd"));
            }
        }

        [TestMethod]
        public void SimpleExpression()
        {

            var temp = "10049205,10043769,10049207,10049213,10049507,10049504,10049203,10049206,10049503,10043694,10049501,10018535,10049210,10049505,10049209,10049214,10011391,10049508,10036345,10049202,10049002,10049032,10023349,10049037,10035045,10017698,10049080,10034092,10049105,10007948,10046219,10028861,10036907,10031743,10039112,10046404,10049138,10019861,10033807,10036183,10022750,10019427,10049437,10013192,10024081,10013205,10049602,10049627,10049629,10049805,10049803,10049620,10049619,10049624,1003882010049608,10049601,10049630,10049628,10049621,10049600,10049606,10049605,10049615,10049700,10000606,10049616,10049724,10049801,10049718,10049604,10049804,10026281,10039564,10045849,10032549,10035412,10025312,10049451,10024072,10031980,10045457,10034704,10032705,10033564,10039573,10049589,10049595,10049710,10049814,10049816,10049712,10049813,10049812,10049609,10049817,10049702,10049810,10049811,10049706,10049705,10049703,10049818,10049708,10036539,10030963,10049672,10038844,10033429,10022628,10032246,10036215,10044315,10050123,10050122,10050124,10050121,10050120,10044277,10035862,10044279,10018186,10038851,10049776,10038120,10030120,10049825,10036774,10034311,10043937,10037880,10000034,10047919,10038361,10029588,10049931,10049980,10035639,10039194,10035824,10049986,10049993,10033801,10050014,10045686,10044260,10037322,10033262,10050502,10024968,10050509,10050511,10049820,10050514,10000111,10050500,10034361,10034376,10030563,10050508,10050513,10050030,10050507,10050504,10050503,10050512,10050034,10026131,10050077,10047153,10050081,10050095,10014838,10043918,10029989,10039601,10033974,10050149,10036098,10035110,10050182,10038174,10017973,10046071,10023683,10037459,10031693,10032687,10030908,10045243,10050240,10050300,10044775,10028825,10050282,10008934,10050303,10030761,10029666,10019872,10035409,10028394,10024481,10045196,10042800,10036761,10039043,10045710,10038751,10034183,10050346,10019486,10045834,10050339,10033141,10050459,10038097,10015569,10031031,10050253,10033414,10019972,10045851,10027085,10027568,10031819,10037595,10018320,10017337,10032848,10050549,10050543,10037401,10039530,10050526,10050527,10031855,10045770,10050584,10044228,10050614,10050617,10050621,10034120,10050615,10048534,10050648,10038144,10050655,10050905,10050902,10051315,10050900,10051320,10051317,10050635,10050632,10050806,10051312,10051313,10050813,10051314,10050821,10050816,10050825,10050803,10050633,10050506,10050805,10050906,10050716,10050823,10045501,10050818,10036460,10050907,10050807,10051323,10050710,10038062,10050804,10050712,10051318,10050824,10050800,10050901,10050812,10046337,10050802,10050808,10050820,10050903,10051319,10031654,10051321,10050904,10050810,10051322,10027737,10050815,10050819,10050745,10019533,10005040,10048973,10029702,10045767,10050762,10037350,10046352,10045601,10050835,10011662,10034049,10025773,10021563,10044719,10034267,10050885,10034108,10050943,10027619,10050498,10037969,10050957,10050998,10051035,10050136,10050961,10050984,10050985,10033687,10051017,10038365,10043630,10032218,10044066,10037484,10051311,10045596,10042815,10050227,10035865,10030007,10051909,10051304,10022674,10051303,10051905,10051907,10051906,10051308,10036757,10051115,10027806,10051910,10051901,10051908,10051302,10051903,10051904,10051300,10051306,10051309,10051307,10051900,10039508,10051305,10032486,10034874,00009,10038368,10031094,10038002,10032277,10016026,10037383,10051381,10021239,10051295,10044398,10051337,10051346,10051968,10051894,10051972,10051916,10051920,10051962,10051984,10051915,10051971,10052004,10051994,10051987,10051936,10051393,10051996,10051860,10051870,10051865,10051902,10051980,10051928,10051974,10051978,10051958,10051913,10051931,10051965,10051938,10051975,10051969,10051898,10051960,10051966,10051967,10051973,10051933,10051861,10051365,10051932,10051934,10052005,10051992,10051985,10051982,10051922,10051930,10051963,10051959,10051941,10051964,10051899,10051961,10051990,10051897,10051927,10051919,10051924,10051970,10051995,10051921,10051378,10051929,10051923,10051866,10051979,10051981,10051893,10051871,10051998,10051361,10051939,10051935,10051895,10051940,10051937,10051918,10051997,10051989,10052003,10051925,10051872,10051868,10051862,10052000,10052001,10051863,10051917,10051983,10036244,10034329,10038013,10051443,10051437,10030253,10035150,10036970,10051482,10038905,10051479,10026200,10051509,10051508,10051562,10051518,10019090,10051857,10028106,10036347,10051642,10052021,10032448,10034496,10052036,10028314,10044310,10046024,10051858,10036056,10052035,10051948,10051856,10051641,10033435,10028326,10052307,10025948,10052304,10049939,10051701,10051679,10052303,10051700,10051702,10052306,10051703,10051954,10052302,10052305,10050811,10052309,10051704,10052308,10044691,10045822,10034550,10051673,10051670,10051671,10051693,10051668,10051677,10051750,10043940,10034340,10051769,10051748,10051849,10051815,10051823,10051798,10050505,10051832,10051834,10051814,10035002,10051838,10051810,10051813,10051850,10052006,10051839,10038871,10051820,10051840,10045923,10051842,10051841,10051844,10035119,10051854,10051881,10051926,10051873,10051889,10051884,10052007,10038314,10045322,10052061,10051892,10051949,10051976,10051942,10044589,10052010,10052020,10052014,10052072,10052070,10052067,10036214,10052060,10052030,10034582,10052027,10052028,10052033,10052025,10052038,10052073,10052041,10052044,10052042,10052039,10052047,10052046,10052043,10052049,10052056,10030070,10030165,10052051,10052065,10052066,10052064,10052075,10052050,10052048,10044622,10052078,10045881,10049320,10052092,10052088,10052068,10052116,10052117,10052096,10052081,10034634,10052101,10052080,10052098,10052107,10052103,10052189,10052112,10052105,10048955,10052115,10052118,10052119,10052123,10052122,10050749,10052120,10052125,10052127,10052134,10052133,10052128,10052253,10052238,10051743,10052250,10046311,10046078,10051745,10043865,10051742,10035194,10052241,10052262,10052252,10052239,10045595,10037517,10052254,10051744,10052158,10052161,10052159,10052144,10052162,10052160,10052245,10052171,10052148,10052165,10052167,10052141,10052301,10052908,10052284,10052145,10052202,10052164,10031709,10052154,10052152,10052156,10032872,10052185,10051053,10052151,10052155,10021148,10048983,10052220,10052324,10052300,10025716,10052176,10052183,10052175,10052170,10052172,10052180,10052174,10052173,10052212,10052198,10052201,10052193,10052191,10052196,10052195,10052192,10035547,10052207,10052211,10049170,10052230,10045632,10052214,10052215,10052229,10052218,10052236,10052242,10052217,10036650,10052256,10052248,10052243,10052271,10052276,10051767,10052225,10011619,10052258,10052257,10043842,10052224,10052227,10052330,10052277,10052327,10052268,10028005,10052255,10052269,10052288,10052278,10052233,10052270,10052329,10052290,10052337,10052289,10052263,10052282,10052264,10052279,10052281,10052314,10017299,10052275,10052283,10052294,10052274,10052389,10052335,10052328,10052326,10021696,10052334,10052333,10052332,10052338,10036246,10052341,10052371,10052340,10052343,10039074,10013578,10052445,10052345,10052348,10052344,10052362,10052360,10052367,10052352,10052358,10052375,10052378,10039065,10052374,10045626,10052366,10052377,10052356,10052369,10052365,10052373,10052357,10044868,10052383,10052376,10052385,10052387,10032368,10052384,10052370,10052400,10027137,10052404,10052394,10052405,10052392,10052388,10052393,10033073,10052391,10052403,10052444,10052416,10052657,10052600,10031264,10044761,10052419,10033496,10052413,10052438,10052452,10052450,10052436,10018639,10052411,10052420,10052412,10052431,10052446,10052503,10034934,10052421,10052414,10052437,10036439,10052425,10052488,10052417,10052453,10052483,10052455,10052456,10052451,10051131,10052458,10052454,10052491,10051242,10052474,10052480,10052460,10052479,10043649,10052470,10052489,10052471,10035826,10044496,10036325,10034931,10052482,10052485,10045700,10052499,10046270,10052858,10032243,10036337,10052492,10052506,10052507,10052505,10052495,10052490,10038117,10052497,10052502,10050454,10020961,10052493,10030854,10052496,10003085,10052498,10052867,10044439,10052532,10052509,10052541,10052552,10052544,10052538,10052512,10052534,10052530,10052535,10052525,10052529,10052571,10052570,10052547,10052549,10052566,10052518,10052555,10052523,10052565,10052545,10052520,10052527,10052562,10052563,10052522,10052560,10052546,10052537,10052557,10052567,10052526,10052604,10052514,10052539,10052510,10052561,10052559,10052542,10052550,10052569,10052521,10052879,10052573,10052618,10052543,10052589,10052591,10052588,10052882,10052866,10052590,10052594,10052585,10014720,10052575,10052878,10052584,10052587,10038831,10052574,10052612,10038828,10052865,10052622,10052603,10052617,10034637,10052616,10038601,10052598,10034792,10052597,10052606,10052602,10052632,10052607,10052634,10052601,10052631,10037339,10052629,10047197,10052626,10052621,10052625,10052654,10054309,00004,10052717,10054307,10054315,10054303,10054314,10054302,10052652,10054313,10054300,10054321,10054305,10052642,10052651,00005,10054317,10054306,10054320,10052468,10054311,10054318,10054308,10054319,10054310,10052467,00002,10054316,10054312,10054301,10054304,10052650,10052753,10052809,10036264,10043944,10045506,10052674,10052644,10052640,10029676,10035969,10052685,10022486,10052863,10052862,10033892,10052864,10052714,10052713,10052731,10052861,10052860,10052673,10022145,10052688,10052675,10052694,10052693,10052443,10052658,10052859,10052690,10052670,10052666,10052656,10052697,10052768,10052648,10052660,10052672,10031324,10032024,10052664,10052647,10052698,10024974,10052667,10037979,10052691,10052744,10052661,10052662,10052663,10010037,10052683,10052679,10052682,10052680,10052659,10052702,10032674,10004233,10052681,10052700,10038118,10050784,10052755,10050687,10052705,10052716,10052905,10052720,10052706,10052807,10052907,10052712,10052903,10052901,10052708,10052900,10052711,10052767,10052904,10052784,10033340,10052719,10052728,10052906,10052704,10052902,10052715,10052726,10052736,10052741,10052721,10019053,10052732,10029647,10052737,10052742,10052725,10052804,10052730,10052806,10052740,10023672,10038285,10052747,10051779,10044436,10052724,10052749,10052748,10052750,10039396,10052751,10052752,10052743,10052759,10052761,10052781,10046268,10052816,10052756,10052760,10052763,10035309,10051808,10052762,10052791,10033279,10052793,10052765,10052758,10052873,10052757,10052773,10052779,10052770,10052774,10052778,10052771,10052777,10052776,10052785,10034957,10052790,10052769,10023020,10052895,10035161,10052639,10028553,10052017,10036310,10052810,10045545,10052801,10052797,10033300,10052819,10052880,10052818,10052838,10052814,10052827,10052829,10052796,10052802,10052821,10044844,10052813,10052883,10052881,10052799,10052828,10052839,10052840,10052808,10046041,10052841,10052842,10052825,10052845,10028771,10052822,10033887,10046112,10052824,10052823,10052918,10052826,10029081,10052854,10038787,10038773,10038770,10046255,10052995,10045247,10038768,10038769,10053060,10052843,10052849,10044070,10052982,10052846,10052990,10035747,10052869,10052853,10038771,10052998,10000425,10032292,10052988,10043641,10049324,10052987,10052852,10052997,10052978,10052996,10021119,10052870,10052981,10052979,10052994,10052986,10052855,10052844,10044577,10052983,10052989,10023506,10052980,10043642,10036318,10052871,10052874,10052894,10052876,10052872,10053107,10052925,10052913,10052892,10052910,10051816,10052899,10052893,10041560,10052885,10052912,10053145,10052884,10052909,10052891,10052916,10052914,10052917,10052787,10052920,10052927,10052837,10052911,10052922,10052956,10052929,10022603,10052928,10052926,10052935,10052934,10052933,10015396,10052946,10037536,10045921,10052945,10024262,10052942,10052938,10052947,10052936,10053059,10050861,10052954,10052953,10036975,10049444,10052955,10053073,10052952,10052950,10052951,10031429,10053105,10052963,10017279,10052949,10053106,10053178,10019202,10053074,10052966,10052973,10052960,10052958,10052970,10015927,10052967,10052968,10053093,10052971,10053092,10053071,10052961,10052975,10052957,10052965,10052962,10053005,10053040,10053010,10053055,10053035,10053086,10053033,10053030,10008629,10053009,10053049,10053075,10053036,10053037,10053017,10053007,10033145,10053020,10053050,10053024,10053047,10053045,10034630,10053019,10053006,10053048,10053052,10053084,10053089,10053042,10053044,10053021,10053039,10053026,10053041,10053000,10053043,10045707,10053025,10053023,10053004,10053027,10053028,10053051,10053056,10053032,10053011,10053054,10020597,10053034,10053038,10032882,10053053,10053076,10053015,10053022,10053070,10052273,10053067,10053063,10053064,10053062,10053069,10053068,10053001,10053061,10034339,10032242,10053066,10053072,10045460,10015365,10053081,10053167,10053080,10053117,10053116,10053147,10044491,10053078,10053121,10053077,10053119,10053118,10053120,10045797,10037785,10053148,10053082,10053207,10051728,10024733,10053088,10053102,10053103,10053219,10053204,10053205,10053290,10044605,10053096,10053099,10053206,10053203,10053208,10045688,10046140,10039332,10019964,10053095,10035904,10046372,10053216,10053097,10023461,10053210,10053218,10053217,10053249,10046229,10053202,10047369,10051732,10046228,10053234,10029416,10002887,10053220,10046386,10053201,10053087,10030152,10053169,10053168,10053213,10053227,10053109,10053112,10053132,10035242,10053108,10053129,10053138,10053110,10053141,10053143,10026454,10053115,10053144,10053130,10053140,10053113,10021910,10053133,10053114,10053170,10053111,10053131,10053135,10053156,10053162,10053155,10053313,10053150,10053157,10053196,10053154,10053174,10053158,10053166,10053153,10039208,10053151,10053146,10053159,10053164,10034576,10053160,10053171,10024595,10053223,10053221,10053180,10053247,10053177,10053214,10053173,10053175,10036942,10053215,10053172,10052457,10053222,10053185,10053191,10053189,10053190,10045711,10053193,10053192,10053187,10053183,10053184,10053186,10053182,10032304,10051731,10053211,10029493,10018151,10034309,10053212,10044221,10053209,10050671,10053232,10053250,10050358,10053292,10053230,10025473,10034375,10025863,10053226,10053229,10053293,10053225,10053240,10053237,10053245,10053242,10053243,10033487,10033803,10053236,10053228,10046592,10053241,10025933,10053265,10053239,10053257,10043974,10053259,10053264,10053261,10053319,10053275,10053260,10053251,10053255,10044872,10053263,10053258,10053262,10053294,10053253,10053282,10053281,10053270,10053276,10053288,10053266,10053269,10053274,10053301,10053268,10052608,10032062,10053278,10053280,10053279,10052147,10035692,1000009,10053299,10053330,10053389,10027263,10053297,10037433,10053295,10044372,10053298,10053332,10053308,10053307,10053306,10053311,10053314,10053304,10053312,10053303,10053305,10045909,10053310,10051031,10053309,10053302,10030178,10053318,10053316,10053329,10053320,10053324,10053325,10036315,10053322,10023774,10050525,10053300,10053323,10053327,10053326,10053331,10053338,10053346,10053339,10053340,10053342,10053343";
            var arr1 = temp.Split(",");
            var rs1 = "";
            foreach (var item1 in arr1)
            {
                rs1 += "'" + item1 + "'" + ",";
            }
            Console.WriteLine(rs1);
            var myDict = new Dictionary<string, string>
            {
                {"00057115","20201109t183002522z001"},
                {"00034435","20201122t193002738z001"},
                {"00019122","20201130t153005264z001"},
                {"00034573","20201130t153005264z001"},
                {"00034495","20201130t153005264z001"},
                {"00053613","20201130t153005264z001"},
                {"00053815","20201130t153005264z001"},
                {"00056728","20201130t153005264z001"},
                {"00008322","20201130t153005264z001"},
                {"00051487","20201130t163009130z001"},
                {"00057138","20201130t203004296z001"}
            };
            var str = "";
            foreach (var data in myDict)
            {
                var d = new DispatchParams();
                d.para = new SyncPersonOA();
                d.para.badges = data.Key;
                d.para.timeStamp = data.Value;
                d.para.forceRefresh = 1;
                d.tenantCode = "ajidou";
                var command = "curl https://gateway.gaiaworkforce.com/hr-cust/api/v1/ajidouSync/personOAv1/ajidou -X POST -d \"" +
                              JsonConvert.SerializeObject(d).Replace("\"", "\\\"") + "\" --header \"Content-Type: application/json\"";
                str += command + ";";
            }
            Console.WriteLine(str);
        }

        [TestMethod]
        public void DicTypeContactString()
        {
            var myDict = new Dictionary<string, string>
            {
{"Z006","HSE Senior Superintendent"},{"Z005","Cleaner"},{"Z004","Crew Leader - Civil Work"},{"Z003","HSE Engineer"},{"Z002","HSE Superintendent"},{"W032","Crew Leader - Warehouse"},{"W029","Shift Leader - Cold End"},{"W026","Warehouse Officer"},{"W025","Planning Officer"},{"W024","Scheduling Superintendent"},{"W022","Cleaner"},{"W017","Forklift Driver"},{"W016","Warehouseman"},{"W011","Warehouse Superintendent"},{"W010","Packer"},{"W009","Sorter Packer"},{"W007","Operator - S & P Machine"},{"W006","Crew Leader - S & P"},{"W003","Clerk"},{"U012","Supplier Quality Engineer"},{"U011","Purchaser"},{"U004","Purchasing Assistant"},{"U001","Purchasing Manager"},{"T061","Technician - Mold Coating"},{"S293","Advanced purchasing and quality management"},{"S292","E-commerce channel manager"},{"S291","National Wholesale Channel Manager"},{"S290","Sales Director"},{"S289","Customer Service Supervisor-IKEA"},{"S287","Assistant Category Manager-Dinner/Kitchenware"},{"S286","Export SA Supervisor"},{"S285","Marketing & BD Manager"},{"S284","Sales Director - Greater South/SE"},{"S283","Communication Executive"},{"S280","Sales Director B/E/Sourcing/Market Understanding"},{"S279","Marketing Categories & NPD Manager"},{"S278","Customers Service Manager"},{"S273","Sales Director-Great North District"},{"S271","Trade Marketing Manager"},{"S270","Domestic SA Supervisor"},{"S262","Premium Designer"},{"S260","Quality Engineer"},{"S256","IKEA Manager"},{"S254","Export Sales/Project Supervisor"},{"S247","Senior Sales Supervisor"},{"S243","Assistant Finance Manager"},{"S221","Sales Supervisor - E-commerce"},{"S184","Sales Manager - Premium & Publicity"},{"S170","APAC Market Controller"},{"S169","Account Manager"},{"S139","Assistant Product Manager"},{"S106","Key Account Manager"},{"S097","Area Sales Manager"},{"S093","Trade Marketing Supervisor"},{"S079","Sales Supervisor –National KA"},{"S077","Sales Supervisor"},{"S013","B TO B Sales Rep"},{"S010","SA Assistant"},{"Q048","Quality/CI Director"},{"Q046","Quality Test Analyst"},{"Q044","Quality Superintendent"},{"Q043","System Manager"},{"Q041","Technician - System"},{"Q040","Shift Leader - Quality"},{"Q039","Quality Inspector - Opal"},{"Q035","CI Engineer"},{"Q033","CI Supervisor"},{"Q020","Quality Engineer"},{"Q018","Technician - Method"},{"Q016","Sourced Product Inspector"},{"Q015","Sourced Product Quality Superintendent"},{"Q006","Quality Inspector"},{"Q005","Crew Leader - Quality"},{"P214","Production Director"},{"P213","Production Quality Improvement Supvisor"},{"P212","Facility & HSE Manager"},{"P211","Mechanical Engineer"},{"P210","Crew Leader - Job Change"},{"P209","Crew Leader - Equipment Mechanical"},{"P207","Shift Supervisor"},{"P206","EA Engineer"},{"P205","R&D Superintendent"},{"P204","Crew Leader - Mold Maintenance"},{"P203","Operator - Sewage Station"},{"P202","Technician - Cold End"},{"P201","Equipment Mechanical Engineer"},{"P200","Junior Automation Technician"},{"P199","Ingredients and laboratory engineer"},{"P198","Technician - Job Change"},{"P196","Mold Process Engineer"},{"P195","Mold Design Engineer"},{"P194","Mold Coating Engineer"},{"P190","Chief Operator - TT"},{"P189","Operator - Mold Coating"},{"P187","Operator - Furnace"},{"P186","Mason Leader"},{"P185","Data Statistician"},{"P184","Job Change Maintenance Man"},{"P182","LNG Maintenance Worker"},{"P180","Chemic Analyst"},{"P177","Equipment Mechanical Superintendent"},{"P176","Forming Mechanical Superintendent"},{"P175","Crew Leader - Machining"},{"P174","Glass Manager"},{"P169","Glass Engineer"},{"P168","Cold End & Finishing Engineer"},{"P165","Hot End Manager"},{"P163","Cold End & Finishing Superintendent"},{"P162","Cold End & Finishing Manager"},{"P161","IL Store Officer"},{"P156","Operator - Utility"},{"P155","Operator - Cold Cut"},{"P154","Operator - Decal"},{"P151","Technician - Mechanical"},{"P149","Crew Leader - Decorating Lab"},{"P147","Mold Design Superintendent"},{"P142","Shift Mold Maintenance Leader"},{"P140","Chief Operator - SEVP"},{"P135","Junior Engineer"},{"P134","Civil Works Engineer"},{"P131","Technical Manager"},{"P129","Crew Leader - Mold QC"},{"P126","Forming Engineer"},{"P125","Fluid Engineer"},{"P122","Crew Leader - Energy Saving"},{"P121","EA Superintendent"},{"P116","Data Statistician"},{"P115","Lab Analyzer"},{"P113","Crew Leader - Finishing"},{"P112","Furnace Engineer"},{"P107","Set-up Man - Stemware"},{"P106","Data Superintendent"},{"P098","Technician - Calibration Tools"},{"P095","Study Office Superintendent"},{"P094","Warehouseman"},{"P093","Crew Leader - Industrial Logistics"},{"P092","Goods Receiving Officer"},{"P089","Industrial Logistics Superintendent"},{"P088","Technician - Automation"},{"P087","Automation Engineer"},{"P084","Technician - Fluid"},{"P081","Crew Leader - Fluid"},{"P080","H/V Station Keeper"},{"P079","Electrician"},{"P078","Crew Leader - Electrical"},{"P076","Mold Controller"},{"P074","Technician - Mold QC"},{"P070","Technician - Mold"},{"P068","Operator - Machine Tool"},{"P067","Tool Dresser"},{"P066","Polisher"},{"P065","Mold Fitter"},{"P064","Crew Leader - Tool"},{"P061","Job Change Man"},{"P059","Mechanic"},{"P058","Crew Leader - Mechanical"},{"P056","Crew Leader - Shift"},{"P052","Operator - Decorating Lab"},{"P040","Operator - FP Manual"},{"P039","Operator - FP Machine"},{"P038","Chief Operator - Finishing"},{"P037","Set-up Man - Finishing"},{"P034","Operator - Forming"},{"P032","Chief Operator - Forming"},{"P028","Set-up Man - Forming"},{"P027","Forming Superintendent"},{"P023","Chemical Engineer"},{"P022","Operator - Batch House"},{"P021","Crew Leader - Batch House"},{"P020","Forklift Driver"},{"P018","Furnace Controller"},{"P017","Mason"},{"P015","Operator - TT"},{"P014","Set-up Man - Energy Saving"},{"P013","Energy Saving Superintendent"},{"P007","Data Statistician"},{"P002","Assistant"},{"N038","NPD Support Supervisor"},{"N035","NPD Supervisor"},{"N031","Senior Design Superintendent"},{"N026","Project Executive"},{"N021","Accessory Engineer"},{"N020","Packaging Engineer"},{"N018","Expertise Engineer"},{"N010","Data Coordinator"},{"N006","Graphic Designer"},{"I015","Public Affairs & Admin Manager"},{"I009","Light Driver"},{"H060","HR Supervisor(HR Operations)"},{"H058","HR Manager (C&B)"},{"H050","C&B Executive"},{"H041","HR Director APAC"},{"H038","C&B Supervisor"},{"H037","Admin Supervisor"},{"H035","Training Supervisor"},{"H024","Administration Assistant"},{"H022","HR Specialist"},{"H016","Crew Leader - Light Driver"},{"H012","Cleaner"},{"F049","Cost Controlling Supervisor"},{"F048","Senior Application Development Engineer"},{"F046","Tax Accountant"},{"F044","Business Systems Director - AITS"},{"F043","IT Helpdesk Engineer"},{"F042","Legal Executive"},{"F038","AR Accountant"},{"F036","AP Accountant"},{"F028","APAC IT Operation Team Leader"},{"F025","SAP BW Consultant"},{"F019","Cost/ Margin Analyst"},{"F013","Accounting Supervisor"},{"F011","Legal Manager"},{"F007","Cashier"},{"F004","Accounting Manager"},{"E039","EWS Logistics Senior Executive"},{"E038","EWS Logistics Manager"},{"E037","EWS Project Superintendent"},{"E036","EWS Mechanical Superintendent"},{"E035","EWS Purchasing Supervisor"},{"E034","EWS Quality Control Superintendent"},{"E033","EWS Logistics Executive"},{"E032","EWS Admin Executive"},{"E031","Crew Leader - Stock Logistics"},{"E030","EWS Manager & Senior Security Manager"},{"E029","Security Manager"},{"E021","Crew Leader - Mechanical"},{"E019","Crew Leader - Electrical"},{"E017","Mechanical Leader"},{"E016","Electrical Leader"},{"E015","Electrical & Automation Superintendent"},{"E014","Deputy Equipment Work Shop Manager"},{"E011","Electrician"},{"E010","Equipment Workshop Controller"},{"E007","Mechanic"},{"E003","Study Office Superintendent"},{"CEO08","APAC CFO"},{"CEO07","APAC CEO"},{"C028","AGC Supply Chain Director"},{"C027","Planning Supervisor - Out Sourcing"},{"C026","Planning Supervisor - In-House"},{"C025","Logistics Coordinator"},{"C024","Crew Leader - Logistics"},{"C023","Logistics Superintendent"},{"C018","Demand Planning Manager"},{"C017","Supply Chain Analyst"},{"C012","SA Assistant"}
            };

            var i = 0;
            var temp = "";
            foreach (var data in myDict)
            {


                temp += "INSERT INTO `lookup_items` (`Id`, `Code`, `Name`, `TypeId`, `DataCollectionsId`, `EffectDate`, `ExpireDate`, `IsValid`, `IsDeleted`, `Order`, `Remark`, `LastUpdateUser`, `LastUpdateTime`, `CreateUser`, `CreateTime`, `IsSystem`) VALUES (UUID(), '" + data.Key + "', '" + data.Value + "', '1069d183-5666-4212-b013-b2268f771493', NULL, '1900-1-1 00:00:00', NULL, 1, 0, " + ++i + ", '', 'Devops', Now(), 'Devops', Now(), 0);";
            }

            // 
            var temp2 = "";
            var myDict2 = new Dictionary<string, string>
            {
{"b818d9f9-6030-4ecf-a983-1c1a08e68600","2021-05-31"},
{"b323fa94-73b2-4246-8725-5a745fc2f0c6","2021-05-31"},
{"cc71716f-d74a-4d4d-b42b-3f2e75e93524","2021-05-31"},
{"edccd488-a707-478e-9093-8a9fd35e1b74","2021-05-31"},
{"ade70e85-1166-4a97-9880-e269a838a6d7","2021-05-31"},
{"ebeaa7e4-d5d8-4c81-8d96-331de369b62c","2021-05-31"},
{"14985db4-e7f5-4531-b876-a3814060a69a","2021-05-31"},
{"057506a5-999c-4530-9e4d-7971432e729f","2021-05-31"},
{"4fb035fb-502c-4db3-82dd-4bf315214be4","2021-05-31"},
{"77b2448f-b01c-4fdd-9efe-17dcac1ed3cd","2021-05-31"},
{"45a7d3c8-46b9-4a2d-b6ff-cf23f942d5b1","2022-10-21"},
{"8dba5684-bcd9-4bf9-a8f4-30b1249e67cb","2021-05-31"},
{"9cbbba7f-d3ac-4b7a-a0bd-268c3e0c4593","2021-05-31"},
{"7cea679e-5873-4b0c-abda-eeda01c78ce5","2021-11-03"},
{"641ec90e-a432-49de-9227-a94a17004c92","2021-05-31"},
{"1b274bc0-50f2-40ac-a393-2db0e8db4e9e","2021-05-31"},
{"4c2728e6-eee4-4b3b-8cd3-ca88c98c1b94","2021-05-31"},
{"77723a6d-91f1-4181-8ee8-282724f75cd5","2021-05-31"},
{"d09f49c1-f40d-4404-8138-5dd11a0604ad","2021-05-31"},
{"5ffe906f-9fc1-496e-9d98-5f491c5a6f19","2021-05-31"},
{"699ad4b3-b2b9-410a-9692-9767765c9f1e","2021-05-31"},
{"d7a4b002-cf55-4d5b-a954-f841d83686c3","2021-05-31"},
{"35030580-232a-459c-99ae-13c7d5932561","2021-05-31"},
{"8a58d34b-b924-4f9e-b09d-5f40e017928f","2021-05-31"},
{"98e274c9-a32a-4315-94db-911ec0e20ebe","2021-05-31"},
{"c1366e63-a904-4883-a1bd-056735faac8b","2021-05-31"},
{"8dbf30cf-00c9-4e5d-a9c1-487fb5d24dd1","2021-05-31"},
{"eb85a9c6-abfa-4d3b-9ac7-736a16839433","2021-05-31"},
{"02c57e8b-9ffe-4b94-9b21-001462d608ff","2021-05-31"},
{"a652c924-6945-4205-92a0-07b3d078f7ed","2021-05-31"},
{"73892008-9e10-42af-93ad-8a51be39bd03","2021-05-31"},
{"891be8f8-034f-402c-b2b8-cf4574b57ece","2021-05-31"},
{"680b003b-b492-41b9-afe6-f7263991c496","2021-05-31"},
{"cf4dd9ea-38d3-4645-8edf-8f8f6c72f111","2021-05-31"},
{"de9a0c57-d776-47af-993e-46bfc2d1a726","2021-05-31"},
{"080da1f2-259a-4a02-b96f-22364c24e06f","2021-05-31"},
{"90638f60-c95c-4a6b-a53a-c8d8fd9217d9","2021-05-31"},
{"dc40dd3f-cf75-4766-8532-64524265807b","2021-05-31"},
{"bb4e4d4a-5f3b-4657-a51b-128d14e84d5c","2021-05-31"},
{"85514d5e-2f74-42d0-8be3-a1171c83a1dc","2021-05-31"},
{"e8624f97-021d-41df-bb2d-0c77ae00a2e0","2021-05-31"},
{"d96c1296-3530-4655-8ee3-4f578ccb6926","2021-05-31"},
{"65f6f6d2-8973-4b72-9de3-7f1c878b4019","2021-05-31"},
{"67260990-b6f3-4c7b-8756-eafa4ef6cca6","2021-05-31"},
{"56f5d242-7659-4998-99c0-88bb951de30d","2021-05-31"},
{"31227c68-0a07-4f3d-a939-447f86f17196","2021-05-31"}

            };
            foreach (var data in myDict2)
            {
                temp2 +=
                    "REPLACE INTO `daktronics_privateeducationinfo` (`Id`, `personId`, `certificateId`, `remark`, `certificateAttachment`, `expireDate`, `isEnable`, `createUser`, `createTime`, `lastUpdateUser`, `lastUpdateTime`, `isDeleted`, `trainingInstitution`, `trainingForm`, `trainAmount`) VALUES (uuid(),  '" +
                    data.Key + "' ,'65e3c9ee-9de3-4092-b60b-9f5079883466', null,null,'" + data.Value +
                    "',0,'admin',now(),'admin',now(),0,null,null,null); ";
            }

        }
        [TestMethod]
        public void ContactString()
        {
            string[] units = { "49973860-97a4-4d13-bd67-0151ef2a0a10", "101" };
            foreach (var unit in units)
            {
                var searchView =
                    "{\"search\": \"{\\\"value\\\":\\\"\\\",\\\"label\\\":\\\"\\\",\\\"type\\\":\\\"\\\",\\\"conditions\\\":[{\\\"key\\\":\\\"unitId\\\",\\\"op\\\":\\\"in\\\",\\\"type\\\":\\\"tree\\\",\\\"value\\\":[\\\"" +
                    unit +
                    "\\\"]},{\\\"key\\\":\\\"status\\\",\\\"op\\\":\\\"in\\\",\\\"value\\\":[\\\"1\\\"]}],\\\"includeChild\\\":false}\"}";

                var criteria = JsonConvert.DeserializeObject<PersonSearchCritteria>(searchView);
                var search = new SearchModel();
                var queryList = new List<SearchContent>();
                if (!string.IsNullOrEmpty(criteria.Search))
                {
                    search = JsonConvert.DeserializeObject<SearchModel>(criteria.Search ?? "");
                }

                if (search.conditions != null)
                {
                    queryList = JsonConvert.DeserializeObject<List<SearchContent>>(search.conditions.ToString());
                }

                if (queryList != null &&
                    queryList.Any(x => x.value != null && !string.IsNullOrWhiteSpace(x.value.ToString())))
                {
                    foreach (var content in queryList.Where(x =>
                        x.value != null && !string.IsNullOrWhiteSpace(x.value.ToString())))
                    {
                        var key = "@" + content.key;
                        var values = string.Empty;
                        JArray contentValue = (content.op.Equals("in")) || (content.op.Equals("btw"))
                            ? (JArray)content.value
                            : new JArray();
                        if (content.op == "all")
                        {
                            continue;
                        }
                    }
                }
            }
        }
    }

    public class SearchContent
    {
        public string key { get; set; }
        public object value { get; set; }
        public string op { get; set; }
        public string type { get; set; }
        public int isSearchCondition { get; set; }
    }

    public class PersonSearchCritteria : PageSearch
    {
        /// <summary>
        /// 用户权限控制的功能编码
        /// </summary>
        public string ActionCode { get; set; }

        public string Search { get; set; }

        public List<Guid> PersonIds { get; set; }
    }

    /// <summary>
    /// 分页查询参数
    /// </summary>
    public class PageSearch
    {
        /// <summary>
        /// 每页显示数量
        /// </summary>
        public int? PageSize { get; set; }

        /// <summary>
        /// 页码
        /// </summary>
        public int? PageIndex { get; set; }
        /// <summary>
        /// 排序字段
        /// </summary>
        public string Sort { get; set; }
    }
    public class SearchModel
    {
        /// <summary>
        /// 主键
        /// </summary>
        public Guid? value { get; set; }
        /// <summary>
        /// 自定义查询名
        /// </summary>
        public string label { get; set; }
        public string search { get; set; }
        /// <summary>
        /// 自定义查询值
        /// </summary>
        public JArray conditions { get; set; }
        /// <summary>
        /// 是否包含子组织
        /// </summary>
        public bool includeChild { get; set; } = false;
    }

    public class SyncPersonOA
    {
        public DateTime? syncDate { get; set; }
        public string badges { get; set; }

        public int? forceRefresh { get; set; }
        public string timeStamp { get; set; }
    }

    public class DispatchParams
    {
        public string tenantCode { get; set; }
        public SyncPersonOA para { get; set; }
    }


}