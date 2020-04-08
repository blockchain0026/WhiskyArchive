using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.VisualBasic;
using Polly;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using WhiskyArchive.Services.WhiskyRecording.API.Extensions;
using WhiskyArchive.Services.WhiskyRecording.Domain.Model.Distilleries;
using WhiskyArchive.Services.WhiskyRecording.Domain.Model.Whiskys;
using WhiskyArchive.Services.WhiskyRecording.Infrastructure;
using WhiskyArchive.Services.WhiskyRecording.Infrastructure.Repositories;

namespace WhiskyArchive.Services.WhiskyRecording.API.Infrasructure
{

    public class WhiskyRecordingContextSeed
    {
        public static async Task SimpleSeedAsync(IApplicationBuilder applicationBuilder, IHostingEnvironment env)
        {

            using (var context = (WhiskyRecordingContext)applicationBuilder
             .ApplicationServices.GetService(typeof(WhiskyRecordingContext)))
            {
                var contentRootPath = env.ContentRootPath;
                var webroot = env.WebRootPath;

                context.Database.Migrate();


                if (!context.Currency.Any())
                {
                    context.Currency.AddRange(GetPredefinedCurrency());
                    await context.SaveChangesAsync();
                }

                if (!context.PriceReference.Any())
                {
                    context.PriceReference.AddRange(GetPredefinedPriceReference());
                    await context.SaveChangesAsync();
                }

                if (!context.Distillerys.Any())
                {
                    context.Distillerys.AddRange(GetPredefinedDistillery());
                    await context.SaveChangesAsync();
                }

                if (!context.Whiskys.Any())
                {



                    context.Whiskys.AddRange(await GetWhiskiesFromFile(context, contentRootPath));

                }
                await context.SaveChangesAsync();
            }
        }


        #region Predefined

        private static IEnumerable<Currency> GetPredefinedCurrency()
        {
            return new List<Currency>()
            {
                Currency.GBP,
                Currency.EUR,
                Currency.USD,
                Currency.TWD,
                Currency.RMB,
                Currency.YEN
            };
        }

        private static IEnumerable<PriceReference> GetPredefinedPriceReference()
        {
            return new List<PriceReference>()
            {
                PriceReference.FacebookPublic,
                PriceReference.FacebookPrivate,
                PriceReference.FacebookAuction,
                PriceReference.EuropeanAuction,
                PriceReference.ChineseAuction,
                PriceReference.JapaneseAuction,
                PriceReference.Other
            };
        }

        private static IEnumerable<Distillery> GetPredefinedDistillery()
        {
            var distilleries = new List<Distillery>();

            #region Distillies
            var glenfarclas = new Distillery(
                new DistilleryName("格蘭花格", "格兰花格", "Glenfarclas"),
                string.Empty,
                string.Empty,
                "1");
            distilleries.Add(glenfarclas);

            var glenlivet = new Distillery(
                new DistilleryName("格蘭利威", "格兰利威", "Glenlivet"),
                string.Empty,
                string.Empty,
                "2");
            distilleries.Add(glenlivet);

            var bowmore = new Distillery(
                new DistilleryName("波摩", "波摩", "bowmore"),
                string.Empty,
                string.Empty,
                "3");
            distilleries.Add(bowmore);


            var highlandPark = new Distillery(
                new DistilleryName("高原騎士", "高原骑士", "Highland Park"),
                string.Empty,
                string.Empty,
                "4");
            distilleries.Add(highlandPark);


            var auchentoshan = new Distillery(
                new DistilleryName("歐肯特軒", "欧肯特轩", "Auchentoshan"),
                string.Empty,
                string.Empty,
                "6");
            distilleries.Add(auchentoshan);


            var longmorn = new Distillery(
                new DistilleryName("龍摩恩", "朗摩", "Longmorn"),
                string.Empty,
                string.Empty,
                "7");
            distilleries.Add(longmorn);


            var glenGrant = new Distillery(
                new DistilleryName("格蘭冠", "格兰冠", "Glen Grant"),
                string.Empty,
                string.Empty,
                "9");
            distilleries.Add(glenGrant);


            var bunnahabhain = new Distillery(
                new DistilleryName("布納哈本", "布纳哈本", "Bunnahabhain"),
                string.Empty,
                string.Empty,
                "10");
            distilleries.Add(bunnahabhain);


            var benriach = new Distillery(
                new DistilleryName("班瑞克", "班瑞克", "Benriach"),
                string.Empty,
                string.Empty,
                "12");
            distilleries.Add(benriach);


            var dalmore = new Distillery(
                new DistilleryName("大摩", "大摩", "Dalmore"),
                string.Empty,
                string.Empty,
                "13");
            distilleries.Add(dalmore);


            var talisker = new Distillery(
                new DistilleryName("泰斯卡", "泰斯卡", "Talisker"),
                string.Empty,
                string.Empty,
                "14");
            distilleries.Add(talisker);


            var glenfiddich = new Distillery(
                new DistilleryName("格蘭菲迪", "格兰菲迪", "Glenfiddich"),
                string.Empty,
                string.Empty,
                "15");
            distilleries.Add(glenfiddich);


            var scapa = new Distillery(
                new DistilleryName("斯卡帕", "斯卡帕", "Scapa"),
                string.Empty,
                string.Empty,
                "17");
            distilleries.Add(scapa);


            var glenglassaugh = new Distillery(
                new DistilleryName("格蘭格拉索", "格兰格拉索", "Glenglassaugh"),
                string.Empty,
                string.Empty,
                "21");
            distilleries.Add(glenglassaugh);


            var bruichladdich = new Distillery(
                new DistilleryName("布萊迪", "布莱迪", "Bruichladdich"),
                string.Empty,
                string.Empty,
                "23");
            distilleries.Add(bruichladdich);


            var macallan = new Distillery(
                new DistilleryName("麥卡倫", "麦卡伦", "Macallan"),
                string.Empty,
                string.Empty,
                "24");
            distilleries.Add(macallan);


            var rosebank = new Distillery(
                new DistilleryName("玫瑰河畔", "罗斯班克", "Rosebank"),
                string.Empty,
                string.Empty,
                "25");
            distilleries.Add(rosebank);


            var clynelish = new Distillery(
                new DistilleryName("克里尼基", "克里尼基", "Clynelish"),
                string.Empty,
                string.Empty,
                "26");
            distilleries.Add(clynelish);


            var springbank = new Distillery(
                new DistilleryName("雲頂", "云顶", "Springbank"),
                string.Empty,
                string.Empty,
                "27");
            distilleries.Add(springbank);


            var laphroaig = new Distillery(
                new DistilleryName("拉佛格", "拉佛格", "Laphroaig"),
                string.Empty,
                string.Empty,
                "29");
            distilleries.Add(laphroaig);


            var glenrothes = new Distillery(
                new DistilleryName("格蘭路思", "格兰路思", "Glenrothes"),
                string.Empty,
                string.Empty,
                "30");
            distilleries.Add(glenrothes);


            var jura = new Distillery(
                new DistilleryName("吉拉", "吉拉", "Isle of Jura"),
                string.Empty,
                string.Empty,
                "31");
            distilleries.Add(jura);


            var edradour = new Distillery(
                new DistilleryName("艾德多爾", "艾德多尔", "Edradour"),
                string.Empty,
                string.Empty,
                "32");
            distilleries.Add(edradour);


            var ardbeg = new Distillery(
                new DistilleryName("阿貝", "阿贝", "Ardbeg"),
                string.Empty,
                string.Empty,
                "33");
            distilleries.Add(ardbeg);

            var glenmoray = new Distillery(
                new DistilleryName("格蘭莫雷", "格兰莫雷", "Glen Moray"),
                string.Empty,
                string.Empty,
                "35");
            distilleries.Add(glenmoray);



            var benrinnes = new Distillery(
                new DistilleryName("本利林", "本利林", "Benrinnes"),
                string.Empty,
                string.Empty,
                "36");
            distilleries.Add(benrinnes);



            var linkwood = new Distillery(
                new DistilleryName("林肯伍德", "林肯伍德", "Linkwood"),
                string.Empty,
                string.Empty,
                "39");
            distilleries.Add(linkwood);



            var balvenie = new Distillery(
                new DistilleryName("百富", "百富", "Balvenie"),
                string.Empty,
                string.Empty,
                "40");
            distilleries.Add(balvenie);



            var dailuaine = new Distillery(
                new DistilleryName("大雲", "大云", "Dailuaine"),
                string.Empty,
                string.Empty,
                "41");
            distilleries.Add(dailuaine);



            var tobermory = new Distillery(
                new DistilleryName("托本莫瑞", "托本莫瑞", "Tobermory"),
                string.Empty,
                string.Empty,
                "42");
            distilleries.Add(tobermory);


            var portEllen = new Distillery(
                new DistilleryName("波特艾倫", "波特艾伦", "Port Ellen"),
                string.Empty,
                string.Empty,
                "43");
            distilleries.Add(portEllen);


            var craigellachie = new Distillery(
                new DistilleryName("魁列奇", "魁列奇", "Craigellachie"),
                string.Empty,
                string.Empty,
                "44");
            distilleries.Add(craigellachie);



            var glenlossie = new Distillery(
                new DistilleryName("格蘭洛希", "格兰洛希", "Glenlossie"),
                string.Empty,
                string.Empty,
                "46");
            distilleries.Add(glenlossie);



            var bladnoch = new Distillery(
                new DistilleryName("布萊德納克", "布莱德纳克", "Bladnoch"),
                string.Empty,
                string.Empty,
                "50");
            distilleries.Add(bladnoch);



            var bushmills = new Distillery(
                new DistilleryName("布什米爾", "布什米尔", "Bushmills"),
                string.Empty,
                string.Empty,
                "51");
            distilleries.Add(bushmills);



            var pulteney = new Distillery(
                new DistilleryName("富特尼", "富特尼", "Old Pulteney"),
                string.Empty,
                string.Empty,
                "52");
            distilleries.Add(pulteney);


            var caolIla = new Distillery(
                new DistilleryName("卡爾里拉", "卡尔里拉", "Caol Ila"),
                string.Empty,
                string.Empty,
                "53");
            distilleries.Add(caolIla);


            var aberlour = new Distillery(
                new DistilleryName("亞伯樂", "亚伯乐", "Aberlour"),
                string.Empty,
                string.Empty,
                "54");
            distilleries.Add(aberlour);



            var royalBrackla = new Distillery(
                new DistilleryName("皇家柏克萊", "皇家柏克莱", "Royal Brackla"),
                string.Empty,
                string.Empty,
                "55");
            distilleries.Add(royalBrackla);



            var strathisla = new Distillery(
                new DistilleryName("史翠艾拉", "史翠艾拉", "Strathisla"),
                string.Empty,
                string.Empty,
                "58");
            distilleries.Add(strathisla);



            var aberfeldy = new Distillery(
                new DistilleryName("艾柏迪", "艾柏迪", "Aberfeldy"),
                string.Empty,
                string.Empty,
                "60");
            distilleries.Add(aberfeldy);



            var brora = new Distillery(
                new DistilleryName("布朗拉", "布朗拉", "Brora"),
                string.Empty,
                string.Empty,
                "61");
            distilleries.Add(brora);



            var glentauchers = new Distillery(
                new DistilleryName("格蘭道奇", "格兰道奇", "Glentauchers"),
                string.Empty,
                string.Empty,
                "63");
            distilleries.Add(glentauchers);



            var ardmore = new Distillery(
                new DistilleryName("阿德莫爾", "阿德莫尔", "Ardmore"),
                string.Empty,
                string.Empty,
                "66");
            distilleries.Add(ardmore);


            var banff = new Distillery(
                new DistilleryName("班夫", "班夫", "Banff"),
                string.Empty,
                string.Empty,
                "67");
            distilleries.Add(banff);



            var blairAthol = new Distillery(
                new DistilleryName("布萊爾阿蘇", "布莱尔阿苏", "Blair Athol"),
                string.Empty,
                string.Empty,
                "68");
            distilleries.Add(blairAthol);


            var balblair = new Distillery(
                new DistilleryName("巴布萊爾", "巴布莱尔", "Balblair"),
                string.Empty,
                string.Empty,
                "70");
            distilleries.Add(balblair);


            var glenburgie = new Distillery(
                new DistilleryName("格蘭柏奇", "格兰柏奇", "Glenburgie"),
                string.Empty,
                string.Empty,
                "71");
            distilleries.Add(glenburgie);


            var miltonduff = new Distillery(
                new DistilleryName("米爾頓道夫", "米尔顿道夫", "Miltonduff"),
                string.Empty,
                string.Empty,
                "72");
            distilleries.Add(miltonduff);


            var aultmore = new Distillery(
                new DistilleryName("雅墨", "雅墨", "Aultmore"),
                string.Empty,
                string.Empty,
                "73");
            distilleries.Add(aultmore);


            var glenuryRoyal = new Distillery(
                new DistilleryName("皇家格蘭烏妮", "格兰乌妮", "Glenury Royal"),
                string.Empty,
                string.Empty,
                "75");
            distilleries.Add(glenuryRoyal);


            var mortlach = new Distillery(
                new DistilleryName("慕赫", "慕赫", "Mortlach"),
                string.Empty,
                string.Empty,
                "76");
            distilleries.Add(mortlach);


            var glenOrd = new Distillery(
                new DistilleryName("格蘭歐", "格兰欧", "Glen Ord"),
                string.Empty,
                string.Empty,
                "77");
            distilleries.Add(glenOrd);


            var benNevis = new Distillery(
                new DistilleryName("班尼富", "班尼富", "Ben Nevis"),
                string.Empty,
                string.Empty,
                "78");
            distilleries.Add(benNevis);


            var deanston = new Distillery(
                new DistilleryName("汀士頓", "汀士顿", "Deanston"),
                string.Empty,
                string.Empty,
                "79");
            distilleries.Add(deanston);


            var glencadam = new Distillery(
                new DistilleryName("格蘭卡登", "格兰卡登", "Glencadam"),
                string.Empty,
                string.Empty,
                "82");
            distilleries.Add(glencadam);


            var glenElgin = new Distillery(
                new DistilleryName("格蘭愛琴", "格兰爱琴", "Glen Elgin"),
                string.Empty,
                string.Empty,
                "85");
            distilleries.Add(glenElgin);


            var speyburn = new Distillery(
                new DistilleryName("詩貝犇", "诗贝奔", "Speyburn"),
                string.Empty,
                string.Empty,
                "88");
            distilleries.Add(speyburn);


            var glenScotia = new Distillery(
                new DistilleryName("格蘭帝", "格兰帝", "Glen Scotia"),
                string.Empty,
                string.Empty,
                "93");
            distilleries.Add(glenScotia);


            var singleton = new Distillery(
                new DistilleryName("蘇格登", "苏格登", "Singleton"),
                string.Empty,
                string.Empty,
                "95");
            distilleries.Add(singleton);


            var glendronach = new Distillery(
                new DistilleryName("格蘭多納", "格兰多纳", "Glendronach"),
                string.Empty,
                string.Empty,
                "96");
            distilleries.Add(glendronach);


            var littlemill = new Distillery(
                new DistilleryName("小磨坊", "小磨坊", "Littlemill"),
                string.Empty,
                string.Empty,
                "97");
            distilleries.Add(littlemill);


            var strathmill = new Distillery(
                new DistilleryName("斯特拉米爾", "斯特拉米尔", "Strathmill"),
                string.Empty,
                string.Empty,
                "100");
            distilleries.Add(strathmill);


            var dalwhinnie = new Distillery(
                new DistilleryName("達爾維尼", "达尔维尼", "Dalwhinnie"),
                string.Empty,
                string.Empty,
                "102");
            distilleries.Add(dalwhinnie);


            var royalLochnagar = new Distillery(
                new DistilleryName("皇家藍勳", "皇家蓝勋", "Royal Lochnagar"),
                string.Empty,
                string.Empty,
                "103");
            distilleries.Add(royalLochnagar);


            var oban = new Distillery(
                new DistilleryName("歐本", "欧本", "Oban"),
                string.Empty,
                string.Empty,
                "110");
            distilleries.Add(oban);


            var lagavulin = new Distillery(
                new DistilleryName("拉加維林", "乐加维林", "Lagavulin"),
                string.Empty,
                string.Empty,
                "111");
            distilleries.Add(lagavulin);

            var lomond = new Distillery(
    new DistilleryName("羅夢湖", "罗梦湖", "Loch Lomond"),
    string.Empty,
    string.Empty,
    "112");
            distilleries.Add(lomond);


            var longrow = new Distillery(
                new DistilleryName("朗格羅", "朗格罗", "Longrow"),
                string.Empty,
                string.Empty,
                "114");
            distilleries.Add(longrow);


            var yoichi = new Distillery(
                new DistilleryName("余市", "余市", "Yoichi"),
                string.Empty,
                string.Empty,
                "116");
            distilleries.Add(yoichi);


            var yamazaki = new Distillery(
                new DistilleryName("山崎", "山崎", "Yamazaki"),
                string.Empty,
                string.Empty,
                "119");
            distilleries.Add(yamazaki);


            var hakushu = new Distillery(
                new DistilleryName("白州", "白州", "Hakushu"),
                string.Empty,
                string.Empty,
                "120");
            distilleries.Add(hakushu);


            var arran = new Distillery(
                new DistilleryName("愛倫", "爱伦", "Arran"),
                string.Empty,
                string.Empty,
                "121");
            distilleries.Add(arran);

            var glengoyne = new Distillery(
                new DistilleryName("格蘭哥尼", "格兰哥尼", "Glengoyne"),
                string.Empty,
                string.Empty,
                "123");
            distilleries.Add(glengoyne);


            var miyagikyo = new Distillery(
                new DistilleryName("宮城峽", "宫城峡", "Miyagikyo"),
                string.Empty,
                string.Empty,
                "124");
            distilleries.Add(miyagikyo);


            var glenmorangie = new Distillery(
                new DistilleryName("格蘭傑", "格兰杰", "Glenmorangie"),
                string.Empty,
                string.Empty,
                "125");
            distilleries.Add(glenmorangie);


            var hazelburn = new Distillery(
                new DistilleryName("赫佐本", "赫佐本", "Hazelburn"),
                string.Empty,
                string.Empty,
                "126");
            distilleries.Add(hazelburn);


            var kilchoman = new Distillery(
                new DistilleryName("齊侯門", "齐侯门", "Kilchoman"),
                string.Empty,
                string.Empty,
                "129");
            distilleries.Add(kilchoman);


            var chichibu = new Distillery(
                new DistilleryName("秩父", "秩父", "Chichibu"),
                string.Empty,
                string.Empty,
                "130");
            distilleries.Add(chichibu);


            var hanyu = new Distillery(
                new DistilleryName("羽生", "羽生", "Hanyu"),
                string.Empty,
                string.Empty,
                "131");
            distilleries.Add(hanyu);


            var karuizawa = new Distillery(
                new DistilleryName("輕井澤", "轻井泽", "Karuizawa"),
                string.Empty,
                string.Empty,
                "132");
            distilleries.Add(karuizawa);


            var chita = new Distillery(
                new DistilleryName("知多", "知多", "Chita"),
                string.Empty,
                string.Empty,
                "G13");
            distilleries.Add(chita);


            var hibiki = new Distillery(
                new DistilleryName("響", "响", "Hibiki"),
                string.Empty,
                string.Empty,
                string.Empty);
            distilleries.Add(hibiki);


            var komagatake = new Distillery(
                new DistilleryName("駒之越", "驹之越", "Komagatake"),
                string.Empty,
                string.Empty,
                string.Empty);
            distilleries.Add(komagatake);


            var kawasaki = new Distillery(
                new DistilleryName("川崎", "川崎", "Kawasaki"),
                string.Empty,
                string.Empty,
                string.Empty);
            distilleries.Add(kawasaki);


            var taketsuru = new Distillery(
                new DistilleryName("竹鶴", "竹鹤", "Taketsuru"),
                string.Empty,
                string.Empty,
                string.Empty);
            distilleries.Add(taketsuru);

            #endregion

            return distilleries;
        }
        #endregion

        #region From Files
        private static async Task<IEnumerable<Whisky>> GetWhiskiesFromFile(WhiskyRecordingContext context, string contentRootPath)
        {
            string checkAddress = Path.Combine(contentRootPath, "Setup");
            string csvFileWhiskies = Path.Combine(contentRootPath, "Setup", "Whiskies.csv");
            if (!Directory.Exists(checkAddress))
                Directory.CreateDirectory(checkAddress);

            using (WebClient webClient = new WebClient())
            {
                webClient.DownloadFile("https://drive.google.com/uc?export=download&id=1gXt5MBHuQry8U0LNgzUhV6IyKE8Vt_j1", csvFileWhiskies);
            }


            if (!File.Exists(csvFileWhiskies))
            {
                Console.WriteLine("No csv found.");
                Console.WriteLine(csvFileWhiskies);
                return null;
            }

            string[] csvheaders;
            try
            {
                string[] requiredHeaders = {
                    "englishname", "chinesename", "distillery", "bottler", "vintage", "statedage", "strength",
                    "size","casktype","casknumber","numberofbottles","rating","fbprice","euprice","chinaprice",
                    "notes","fbpricedate","eupricedate","chinapricedate","dateupdated"
                };
                csvheaders = GetHeaders(requiredHeaders, csvFileWhiskies);
            }


            catch (Exception ex)
            {
                return null;
            }

            var r = System.Text.Encoding.GetEncoding("gb2312");
            List<Whisky> whiskies = File.ReadAllLines(csvFileWhiskies, System.Text.Encoding.UTF8)
                        .Skip(1) // skip header column
                        .Select(row => Regex.Split(row, ",(?=(?:[^\"]*\"[^\"]*\")*[^\"]*$)"))
                        .SelectTry(column => CreateWhisky(context, column, csvheaders))
                        .OnCaughtException(ex => { return null; })
                        .Where(x => x != null)
                        .ToList();

            return whiskies;
        }

        private static Whisky CreateWhisky(WhiskyRecordingContext context, string[] column, string[] headers)
        {
            if (column.Count() != headers.Count())
            {
                throw new Exception($"column count '{column.Count()}' not the same as headers count'{headers.Count()}'");
            }

            /*string cardtypeString = column[Array.IndexOf(headers, "cardtype")].Trim('"').Trim();
            if (!int.TryParse(cardtypeString, out int cardtype))
            {
                throw new Exception($"cardtype='{cardtypeString}' is not a number");
            }*/
            var distilleryName = column[Array.IndexOf(headers, "distillery")].Trim('"').Trim();

            var existingDistillery = context.Distillerys.Where(d => d.DistilleryName.English.ToLower() == distilleryName.ToLower()).SingleOrDefault();
            if (existingDistillery == null)
            {
                throw new Exception($"Distillery not found with name {distilleryName}");
            }
            try
            {
                var englishname = column[Array.IndexOf(headers, "englishname")].Trim('"').Trim();
                var chinesename = column[Array.IndexOf(headers, "chinesename")].Trim('"').Trim();
                var distillery = column[Array.IndexOf(headers, "distillery")].Trim('"').Trim();
                var bottler = column[Array.IndexOf(headers, "bottler")].Trim('"').Trim();
                var vintage = column[Array.IndexOf(headers, "vintage")].Trim('"').Trim();
                var statedage = column[Array.IndexOf(headers, "statedage")].Trim('"').Trim();
                var strength = column[Array.IndexOf(headers, "strength")].Trim('"').Trim();
                var size = column[Array.IndexOf(headers, "size")].Trim('"').Trim();
                var casktype = column[Array.IndexOf(headers, "casktype")].Trim('"').Trim();
                var casknumber = column[Array.IndexOf(headers, "casknumber")].Trim('"').Trim();
                var numberofbottles = column[Array.IndexOf(headers, "numberofbottles")].Trim('"').Trim();
                var rating = column[Array.IndexOf(headers, "rating")].Trim('"').Trim();
                var fbprice = column[Array.IndexOf(headers, "fbprice")].Trim('"').Trim();
                var euprice = column[Array.IndexOf(headers, "euprice")].Trim('"').Trim();
                var chinaprice = column[Array.IndexOf(headers, "chinaprice")].Trim('"').Trim();
                var notes = column[Array.IndexOf(headers, "notes")].Trim('"').Trim();
                var fbpricedate = column[Array.IndexOf(headers, "fbpricedate")].Trim('"').Trim();
                var eupricedate = column[Array.IndexOf(headers, "eupricedate")].Trim('"').Trim();
                var chinapricedate = column[Array.IndexOf(headers, "chinapricedate")].Trim('"').Trim();
                var dateupdated = column[Array.IndexOf(headers, "dateupdated")].Trim('"').Trim();

                var whisky = Whisky.From(
                    existingDistillery,
                    chinesename,
                    whiskyNameEnglish: englishname,
                    whiskyBottler: bottler,
                    vintage: vintage,
                    statedAge: int.TryParse(statedage, out int statedageN) ? statedageN : 0,
                    caskType: casktype,
                    caskNumber: casknumber,
                    numberOfBottles: int.TryParse(numberofbottles, out int numberofbottlesN) ? numberofbottlesN : 0,
                    strength: float.TryParse(strength, out float strengthN) ? strengthN : 0,
                    size: int.TryParse(size, out int sizeN) ? sizeN : 0,
                    rating: float.TryParse(rating, out float ratingN) ? ratingN : 0,
                    notes: notes
                    );
                //var fbpricedate = column[Array.IndexOf(headers, "fbpricedate")].Trim('"').Trim();
                /*if (decimal.TryParse(column[Array.IndexOf(headers, "fbprice")].Trim('"').Trim(), out decimal fbprice) &&
                    !string.IsNullOrEmpty(fbpricedate))
                {

                }*/

                DateTime dt;
                if (decimal.TryParse(fbprice, out decimal fbpriceN) &&
                    DateTime.TryParseExact(fbpricedate, "yyyy/M/d", CultureInfo.InvariantCulture, DateTimeStyles.None, out dt))
                {
                    int refId = PriceReference.FacebookAuction.Id;
                    if (notes.Contains("明價"))
                    {
                        refId = PriceReference.FacebookPublic.Id;
                    }
                    if (notes.Contains("私價"))
                    {
                        refId = PriceReference.FacebookPrivate.Id;
                    }
                    whisky.UpdatePrice(fbpriceN, Currency.TWD.Id, refId, dt);
                }

                if (decimal.TryParse(euprice, out decimal eupriceN) &&
                    DateTime.TryParseExact(eupricedate, "yyyy/M/d", CultureInfo.InvariantCulture, DateTimeStyles.None, out dt))
                {
                    whisky.UpdatePrice(eupriceN, Currency.GBP.Id, PriceReference.EuropeanAuction.Id, dt);
                }


                return whisky;
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
                return null;
            }



        }

        #endregion
        private static string[] GetHeaders(string[] requiredHeaders, string csvfile)
        {
            string[] csvheaders = File.ReadLines(csvfile).First().ToLowerInvariant().Split(',');

            if (csvheaders.Count() != requiredHeaders.Count())
            {
                throw new Exception($"requiredHeader count '{ requiredHeaders.Count()}' is different then read header '{csvheaders.Count()}'");
            }

            foreach (var requiredHeader in requiredHeaders)
            {
                if (!csvheaders.Contains(requiredHeader))
                {
                    throw new Exception($"does not contain required header '{requiredHeader}'");
                }
            }

            return csvheaders;
        }

        public static string GetCSV(string url)
        {
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
            HttpWebResponse resp = (HttpWebResponse)req.GetResponse();

            StreamReader sr = new StreamReader(resp.GetResponseStream());
            string results = sr.ReadToEnd();
            sr.Close();

            return results;
        }



        private Policy CreatePolicy(ILogger<WhiskyRecordingContextSeed> logger, string prefix, int retries = 3)
        {
            return Policy.Handle<SqlException>().
                WaitAndRetryAsync(
                    retryCount: retries,
                    sleepDurationProvider: retry => TimeSpan.FromSeconds(5),
                    onRetry: (exception, timeSpan, retry, ctx) =>
                    {
                        logger.LogTrace($"[{prefix}] Exception {exception.GetType().Name} with message ${exception.Message} detected on attempt {retry} of {retries}");
                    }
                );
        }
    }
}
