using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.ServiceModel.Syndication;
using System.ServiceModel;
using System.Web;
using System.Windows;
using System.Net;
using System.Xml.Linq;
using ConsoleRSS.EF;
using ConsoleRSS.Models;
using System.Text.RegularExpressions;
using System.Data.Entity;


namespace ConsoleRSS
{
    class Program
    {

        static int UpdateDB(string url)//Запись в БД
        {
            Context db = new Context();
           

                int count = 0;

                int all = db.RSSNews.Where(cfd=>cfd.RSSSource.RSSSourceURL==url).Count();


                XmlReader reader = XmlReader.Create(url);
                try
                {


                    SyndicationFeed rssdata = SyndicationFeed.Load(reader);

                    foreach (var item in rssdata.Items)
                    {
                    
                        if (db.RSSNews.Where(x => x.NewsName == item.Title.Text.ToString()).FirstOrDefault() == null && db.RSSNews.Where(d => d.Date == item.PublishDate.DateTime).FirstOrDefault() == null)
                        {
                            count++;
                            db.RSSNews.Add(new RSSNews { Date = item.PublishDate.DateTime, Description = Regex.Replace(item.Summary.Text.ToString(), @"<[^>]*>", String.Empty), NewsName = item.Title.Text.ToString(), NewsURL = item.Id.ToString(), RSSSourceId = db.RSSSources.Where(x => x.RSSSourceURL == url).FirstOrDefault().Id });

                        }
                    }

                }
                catch
                {
                    Console.WriteLine("Источни: " + url + " недоступен");

                }
                finally
                {
                    reader.Close();
                }





                db.SaveChanges();
            Console.WriteLine("\nДобавлено: " + count);
                return count+all;
            
            
        }
        static void Main(string[] args)
        {

            Context db = new Context();


            foreach (var item in db.RSSSources)//Вывод на экран

            {
                int count =  UpdateDB(item.RSSSourceURL.ToString());
                Console.WriteLine("Источник: "+item.SourceName+"\nВсего новостей в истчнике: "+count);
                Console.WriteLine(new string('-',50));
            }

            foreach (var item in db.RSSNews.Include(cfg => cfg.RSSSource))
            {

                //Console.WriteLine("Заголовок: " + item.NewsName);

                //Console.WriteLine("Дата публикации: " + item.Date.ToString());

                //Console.WriteLine("Описание: " + item.Description);

                //Console.WriteLine("URL новости: " + item.NewsURL);

                //Console.WriteLine("Название источника" + item.RSSSource.SourceName);
                //Console.WriteLine("URL источника" + item.RSSSource.RSSSourceURL);
                //Console.WriteLine(new string('-', 55));
            }

          
            Console.ReadLine();
        }
    }
}
