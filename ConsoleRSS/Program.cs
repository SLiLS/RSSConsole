using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using System.ServiceModel.Syndication;
using ConsoleRSS.EF;
using ConsoleRSS.Models;
using System.Text.RegularExpressions;
using System.Data.Entity;
using ConsoleRSS.Repositories;

namespace ConsoleRSS
{
    class Program
    {
        
       static  int UpdateDB(string url)
        {
            int count = 0;
           Context db = new Context();
            {
                RSSNewsRepository newsrepository = new RSSNewsRepository(db);
                RSSSourcesRepository sourcesRepository = new RSSSourcesRepository(db);
               
                XmlReader reader = XmlReader.Create(url);
                try
                {
                    SyndicationFeed rssdata = SyndicationFeed.Load(reader);
                    foreach (var item in rssdata.Items)
                    {

                        if (newsrepository.Check(item.PublishDate.DateTime, item.Title.Text.ToString()))
                        {
                            count++;
                            newsrepository.Create((new RSSNews
                            {
                                Date = item.PublishDate.DateTime,
                                Description = Regex.Replace(item.Summary.Text.ToString(),
                                @"<[^>]*>", String.Empty),
                                NewsName = item.Title.Text.ToString(),
                                NewsURL = item.Id.ToString(),
                                RSSSourceId = sourcesRepository.GetAll().Where(x => x.RSSSourceURL == url).FirstOrDefault().Id
                            }));

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
                newsrepository.Save();
                Console.WriteLine("Всего новстей в истонике: "+newsrepository.GetAll().Where(s=>s.RSSSource.RSSSourceURL==url).Count());
            }

            return count;
        }


        static void Main(string[] args)
        {
            Context db = new Context();
            RSSNewsRepository newsrepository = new RSSNewsRepository(db);
            RSSSourcesRepository sourcesRepository = new RSSSourcesRepository(db);

           
            IEnumerable<RSSNews> news = newsrepository.GetAll();
            IEnumerable<RSSSource> sources = sourcesRepository.GetAll();
           
            foreach (var item in sources)
            {
                
                int updated=  UpdateDB(item.RSSSourceURL);
                Console.WriteLine("Источник :" + item.SourceName);
                Console.WriteLine("Добавлено новостей: {0}", updated);
                
               
                Console.WriteLine(new string('-',50));
            }
           
        
            Console.ReadLine();
        }
    }
}
