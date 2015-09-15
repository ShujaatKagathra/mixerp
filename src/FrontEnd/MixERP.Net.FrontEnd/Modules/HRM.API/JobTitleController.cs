using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MixERP.Net.ApplicationState.Cache;
using MixERP.Net.Common.Extensions;
using MixERP.Net.EntityParser;
using Newtonsoft.Json;
using PetaPoco;

namespace MixERP.Net.Api.HRM
{
    /// <summary>
    ///     Provides a direct HTTP access to perform various tasks such as adding, editing, and removing Job Titles.
    /// </summary>
    [RoutePrefix("api/v1.5/hrm/job-title")]
    public class JobTitleController : ApiController
    {
        /// <summary>
        ///     The JobTitle data context.
        /// </summary>
        private readonly MixERP.Net.Core.Modules.HRM.Data.JobTitle JobTitleContext;

        public JobTitleController()
        {
            this.LoginId = AppUsers.GetCurrent().View.LoginId.ToLong();
            this.UserId = AppUsers.GetCurrent().View.UserId.ToInt();
            this.OfficeId = AppUsers.GetCurrent().View.OfficeId.ToInt();
            this.Catalog = AppUsers.GetCurrentUserDB();

            this.JobTitleContext = new MixERP.Net.Core.Modules.HRM.Data.JobTitle
            {
                Catalog = this.Catalog,
                LoginId = this.LoginId
            };
        }

        public long LoginId { get; }
        public int UserId { get; private set; }
        public int OfficeId { get; private set; }
        public string Catalog { get; }

        /// <summary>
        ///     Counts the number of job titles.
        /// </summary>
        /// <returns>Returns the count of the job titles.</returns>
        [AcceptVerbs("GET", "HEAD")]
        [Route("count")]
        [Route("~/api/hrm/job-title/count")]
        public long Count()
        {
            try
            {
                return this.JobTitleContext.Count();
            }
            catch (UnauthorizedException)
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.Forbidden));
            }
            catch
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.InternalServerError));
            }
        }

        /// <summary>
        ///     Returns an instance of job title.
        /// </summary>
        /// <param name="jobTitleId">Enter JobTitleId to search for.</param>
        /// <returns></returns>
        [AcceptVerbs("GET", "HEAD")]
        [Route("{jobTitleId}")]
        [Route("~/api/hrm/job-title/{jobTitleId}")]
        public MixERP.Net.Entities.HRM.JobTitle Get(int jobTitleId)
        {
            try
            {
                return this.JobTitleContext.Get(jobTitleId);
            }
            catch (UnauthorizedException)
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.Forbidden));
            }
            catch
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.InternalServerError));
            }
        }

        [AcceptVerbs("GET", "HEAD")]
        [Route("get")]
        [Route("~/api/hrm/job-title/get")]
        public IEnumerable<MixERP.Net.Entities.HRM.JobTitle> Get([FromUri] int[] jobTitleIds)
        {
            try
            {
                return this.JobTitleContext.Get(jobTitleIds);
            }
            catch (UnauthorizedException)
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.Forbidden));
            }
            catch
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.InternalServerError));
            }
        }

        /// <summary>
        ///     Creates a paginated collection containing 25 job titles on each page, sorted by the property JobTitleId.
        /// </summary>
        /// <returns>Returns the first page from the collection.</returns>
        [AcceptVerbs("GET", "HEAD")]
        [Route("")]
        [Route("~/api/hrm/job-title")]
        public IEnumerable<MixERP.Net.Entities.HRM.JobTitle> GetPagedResult()
        {
            try
            {
                return this.JobTitleContext.GetPagedResult();
            }
            catch (UnauthorizedException)
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.Forbidden));
            }
            catch
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.InternalServerError));
            }
        }

        /// <summary>
        ///     Creates a paginated collection containing 25 job titles on each page, sorted by the property JobTitleId.
        /// </summary>
        /// <param name="pageNumber">Enter the page number to produce the resultset.</param>
        /// <returns>Returns the requested page from the collection.</returns>
        [AcceptVerbs("GET", "HEAD")]
        [Route("page/{pageNumber}")]
        [Route("~/api/hrm/job-title/page/{pageNumber}")]
        public IEnumerable<MixERP.Net.Entities.HRM.JobTitle> GetPagedResult(long pageNumber)
        {
            try
            {
                return this.JobTitleContext.GetPagedResult(pageNumber);
            }
            catch (UnauthorizedException)
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.Forbidden));
            }
            catch
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.InternalServerError));
            }
        }

        /// <summary>
        ///     Creates a filtered and paginated collection containing 25 job titles on each page, sorted by the property JobTitleId.
        /// </summary>
        /// <param name="pageNumber">Enter the page number to produce the resultset.</param>
        /// <param name="filters">The list of filter conditions.</param>
        /// <returns>Returns the requested page from the collection using the supplied filters.</returns>
        [AcceptVerbs("POST")]
        [Route("get-where/{pageNumber}")]
        [Route("~/api/hrm/job-title/get-where/{pageNumber}")]
        public IEnumerable<MixERP.Net.Entities.HRM.JobTitle> GetWhere(long pageNumber, [FromBody]dynamic filters)
        {
            try
            {
                List<EntityParser.Filter> f = JsonConvert.DeserializeObject<List<EntityParser.Filter>>(filters);
                return this.JobTitleContext.GetWhere(pageNumber, f);
            }
            catch (UnauthorizedException)
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.Forbidden));
            }
            catch
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.InternalServerError));
            }
        }

        /// <summary>
        ///     Displayfield is a lightweight key/value collection of job titles.
        /// </summary>
        /// <returns>Returns an enumerable key/value collection of job titles.</returns>
        [AcceptVerbs("GET", "HEAD")]
        [Route("display-fields")]
        [Route("~/api/hrm/job-title/display-fields")]
        public IEnumerable<DisplayField> GetDisplayFields()
        {
            try
            {
                return this.JobTitleContext.GetDisplayFields();
            }
            catch (UnauthorizedException)
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.Forbidden));
            }
            catch
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.InternalServerError));
            }
        }

        /// <summary>
        ///     A custom field is a user defined field for job titles.
        /// </summary>
        /// <returns>Returns an enumerable custom field collection of job titles.</returns>
        [AcceptVerbs("GET", "HEAD")]
        [Route("custom-fields")]
        [Route("~/api/hrm/job-title/custom-fields")]
        public IEnumerable<PetaPoco.CustomField> GetCustomFields()
        {
            try
            {
                return this.JobTitleContext.GetCustomFields(null);
            }
            catch (UnauthorizedException)
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.Forbidden));
            }
            catch
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.InternalServerError));
            }
        }

        /// <summary>
        ///     A custom field is a user defined field for job titles.
        /// </summary>
        /// <returns>Returns an enumerable custom field collection of job titles.</returns>
        [AcceptVerbs("GET", "HEAD")]
        [Route("custom-fields")]
        [Route("~/api/hrm/job-title/custom-fields/{resourceId}")]
        public IEnumerable<PetaPoco.CustomField> GetCustomFields(string resourceId)
        {
            try
            {
                return this.JobTitleContext.GetCustomFields(resourceId);
            }
            catch (UnauthorizedException)
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.Forbidden));
            }
            catch
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.InternalServerError));
            }
        }

        /// <summary>
        ///     Adds or edits your instance of JobTitle class.
        /// </summary>
        /// <param name="jobTitle">Your instance of job titles class to add or edit.</param>
        [AcceptVerbs("PUT")]
        [Route("add-or-edit")]
        [Route("~/api/hrm/job-title/add-or-edit")]
        public void AddOrEdit([FromBody]MixERP.Net.Entities.HRM.JobTitle jobTitle)
        {
            if (jobTitle == null)
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.MethodNotAllowed));
            }

            try
            {
                this.JobTitleContext.AddOrEdit(jobTitle);
            }
            catch (UnauthorizedException)
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.Forbidden));
            }
            catch
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.InternalServerError));
            }
        }

        /// <summary>
        ///     Adds your instance of JobTitle class.
        /// </summary>
        /// <param name="jobTitle">Your instance of job titles class to add.</param>
        [AcceptVerbs("POST")]
        [Route("add/{jobTitle}")]
        [Route("~/api/hrm/job-title/add/{jobTitle}")]
        public void Add(MixERP.Net.Entities.HRM.JobTitle jobTitle)
        {
            if (jobTitle == null)
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.MethodNotAllowed));
            }

            try
            {
                this.JobTitleContext.Add(jobTitle);
            }
            catch (UnauthorizedException)
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.Forbidden));
            }
            catch
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.InternalServerError));
            }
        }

        /// <summary>
        ///     Edits existing record with your instance of JobTitle class.
        /// </summary>
        /// <param name="jobTitle">Your instance of JobTitle class to edit.</param>
        /// <param name="jobTitleId">Enter the value for JobTitleId in order to find and edit the existing record.</param>
        [AcceptVerbs("PUT")]
        [Route("edit/{jobTitleId}")]
        [Route("~/api/hrm/job-title/edit/{jobTitleId}")]
        public void Edit(int jobTitleId, [FromBody] MixERP.Net.Entities.HRM.JobTitle jobTitle)
        {
            if (jobTitle == null)
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.MethodNotAllowed));
            }

            try
            {
                this.JobTitleContext.Update(jobTitle, jobTitleId);
            }
            catch (UnauthorizedException)
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.Forbidden));
            }
            catch
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.InternalServerError));
            }
        }

        /// <summary>
        ///     Deletes an existing instance of JobTitle class via JobTitleId.
        /// </summary>
        /// <param name="jobTitleId">Enter the value for JobTitleId in order to find and delete the existing record.</param>
        [AcceptVerbs("DELETE")]
        [Route("delete/{jobTitleId}")]
        [Route("~/api/hrm/job-title/delete/{jobTitleId}")]
        public void Delete(int jobTitleId)
        {
            try
            {
                this.JobTitleContext.Delete(jobTitleId);
            }
            catch (UnauthorizedException)
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.Forbidden));
            }
            catch
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.InternalServerError));
            }
        }


    }
}