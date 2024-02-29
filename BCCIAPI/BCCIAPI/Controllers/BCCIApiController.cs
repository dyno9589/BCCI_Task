using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using DAL;
using Newtonsoft.Json;


namespace BCCIAPI.Controllers
{
    public class BCCIApiController : ApiController
    {
        //get all matches
        public HttpResponseMessage Get()
        {
            using (BCCIDBEntities entities = new BCCIDBEntities())
            {

                return Request.CreateResponse(HttpStatusCode.OK, entities.BcciSeries.ToList());

            }

        }

        //public IEnumerable<BcciSery> Get()
        //{
        //    using (BCCIDBEntities entities = new BCCIDBEntities())
        //    {

        //        return entities.BcciSeries.ToList();

        //    }

        //}



        //get match by id
        [HttpGet]
        public HttpResponseMessage GetById(int id)
        {
            using (BCCIDBEntities entities = new BCCIDBEntities())
            {
                //var entity = entities.BcciSeries.FirstOrDefault(e => e.SeriesId == id);

                var entity =entities.BcciSeries.FirstOrDefault(e=>e.MatchId == id);

                if (entity != null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, entity);
                }
                else 
                {
                    //return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Series with Id= " + id.ToString() + "not found");

                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Match with Id= " + id.ToString() + "not found");
                }
            }
        }


        //create match
        public HttpResponseMessage Post([FromBody] BcciSery bcciSery) 
        {
            try
            {
                using (BCCIDBEntities entities = new BCCIDBEntities())
                {
                    entities.BcciSeries.Add(bcciSery);
                    entities.SaveChanges();

                    var message = Request.CreateResponse(HttpStatusCode.Created, bcciSery);
                    message.Headers.Location = new Uri(Request.RequestUri + bcciSery.MatchId.ToString());
                    return message;
                }
            } 
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);

            }
        }


        //delete matches by id
        public HttpResponseMessage Delete(int id)
        {
            try
            {
                using (BCCIDBEntities entities = new BCCIDBEntities())
                {
                    var entity = entities.BcciSeries.FirstOrDefault(e=>e.MatchId == id);

                    if (entity == null)
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Match with Id " + id.ToString() + " not found to delete");
                    }
                    else 
                    {
                        entities.BcciSeries.Remove(entity);
                        entities.SaveChanges() ;
                        return Request.CreateResponse(HttpStatusCode.OK);
                    }
                }
            }
            catch (Exception ex) 
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }


        //edit matches
        public HttpResponseMessage Put(int id, [FromBody] BcciSery bcciSery)
        {
            try
            {
                using (BCCIDBEntities entities = new BCCIDBEntities())
                {
                    var entity = entities.BcciSeries.FirstOrDefault(e => e.MatchId == id);
                    if (entity == null)
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Match with Id " + id.ToString() + " not found to update");
                    }
                    else 
                    { 
                        entity.MatchName=bcciSery.MatchName;
                        entity.Team1=bcciSery.Team1;
                        entity.Team2=bcciSery.Team2;
                        entity.MatchDate=bcciSery.MatchDate;
                        entity.Venue=bcciSery.Venue;
                        entity.SeriesId=bcciSery.SeriesId;
                        entity.SeriesName=bcciSery.SeriesName;
                        entity.SeriesStartDate=bcciSery.SeriesStartDate;
                        entity.SeriesEndDate=bcciSery.SeriesEndDate;

                        entities.SaveChanges();

                        return Request.CreateResponse(HttpStatusCode.OK, entity);

                    }
                }
            }
            catch (Exception ex) 
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);

            }
        }

    }
}
