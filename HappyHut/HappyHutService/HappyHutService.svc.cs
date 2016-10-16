using BussinessObjects;
using LogUtils;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;
using System.Text;

namespace HappyHutService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class HappyHutService : IHappyHutService
    {
        public bool SetCustomerRequestInfo(GetQuoteInfo inf, HappyHutDataBaseEntities Entities = null)
        {
            bool bFlag = false;
            DateTime now = DateTime.UtcNow;
            HappyHutDataBaseEntities entities = null;
            try
            {
                entities = Entities == null ? new HappyHutDataBaseEntities() : Entities;

                GetQuoteRequest dat = entities.GetQuoteRequests.SingleOrDefault(x => x.Id.Equals(inf.Id));

                if (dat == null)
                {
                    dat = new GetQuoteRequest()
                    {
                        Id = Guid.NewGuid(),
                        CreateDt = now,
                    };
                    entities.GetQuoteRequests.Add(dat);
                }
                dat.FirstName = !String.IsNullOrEmpty(inf.FirstName) ? inf.FirstName : String.Empty;
                dat.LastName = !String.IsNullOrEmpty(inf.LastName) ? inf.LastName : String.Empty;
                dat.Email = !String.IsNullOrEmpty(inf.Email) ? inf.Email : String.Empty;
                dat.MobileNumber = !String.IsNullOrEmpty(inf.MobileNumber) ? inf.MobileNumber : String.Empty;
                dat.ServiceId = inf.ServiceId;
                dat.IsEmailSent = inf.IsEmailSent;
                dat.EmailSentDt = inf.EmailSentDt;
                dat.PreferredDate = inf.PreferredDate;
                dat.PreferredTime = inf.PreferredTime;
                dat.AdditionalInfo = !String.IsNullOrEmpty(inf.AdditionalInfo) ? inf.AdditionalInfo : String.Empty;
                dat.LastUpdateDt = now;


                if (Entities == null)
                    entities.SaveChanges();
                bFlag = true;
            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    ApplicationLog.Instance.WriteError(String.Format("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State));
                    foreach (var ve in eve.ValidationErrors)
                    {
                        ApplicationLog.Instance.WriteError(String.Format("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage));
                    }
                }
            }
            catch (Exception ex)
            {
                ApplicationLog.Instance.WriteException(ex);
            }
            finally
            {
                if (Entities == null)
                    entities.Dispose();
            }
            return bFlag;
        }

        public bool SetCustomerRequestInfos(List<GetQuoteInfo> infs, HappyHutDataBaseEntities Entities = null)
        {
            bool bFlag = false;
            DateTime now = DateTime.UtcNow;
            HappyHutDataBaseEntities entities = null;
            try
            {
                entities = Entities == null ? new HappyHutDataBaseEntities() : Entities;

                foreach (GetQuoteInfo inf in infs)
                {
                    SetCustomerRequestInfo(inf, entities);
                }
                return true;
            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    ApplicationLog.Instance.WriteError(String.Format("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State));
                    foreach (var ve in eve.ValidationErrors)
                    {
                        ApplicationLog.Instance.WriteError(String.Format("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage));
                    }
                }
            }
            catch (Exception ex)
            {
                ApplicationLog.Instance.WriteException(ex);
            }
            finally
            {
                if (Entities == null)
                    entities.Dispose();
            }
            return bFlag;
        }

        public List<AreaInCityInfo> GetAreasInCity(int cid, HappyHutDataBaseEntities Entities = null)
        {
            List<AreaInCityInfo> lst = new List<AreaInCityInfo>();
            HappyHutDataBaseEntities entities = null;
            try
            {
                entities = Entities == null ? new HappyHutDataBaseEntities() : Entities;

                lst = (from dat in entities.AreasInCities
                       where dat.CityID.Equals(cid)
                       orderby dat.Name
                       select new AreaInCityInfo()
                       {
                           ID = dat.ID,
                           Name = dat.Name,
                           CityID = dat.CityID
                       }).ToList();
            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    ApplicationLog.Instance.WriteError(String.Format("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State));
                    foreach (var ve in eve.ValidationErrors)
                    {
                        ApplicationLog.Instance.WriteError(String.Format("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage));
                    }
                }
            }
            catch (Exception ex)
            {
                ApplicationLog.Instance.WriteException(ex);
            }
            finally
            {
                if (Entities == null)
                    entities.Dispose();
            }
            return lst;
        }

        public List<ServiceInAreaInfo> GetServicesInArea(int areaid, HappyHutDataBaseEntities Entities = null)
        {
            List<ServiceInAreaInfo> lst = new List<ServiceInAreaInfo>();
            HappyHutDataBaseEntities entities = null;
            try
            {
                entities = Entities == null ? new HappyHutDataBaseEntities() : Entities;

                lst = (from dat in entities.ServicesInAreas
                       where dat.AreaID.Equals(areaid) && dat.IsActive
                       select new ServiceInAreaInfo()
                       {
                           ServiceInAreaId = dat.Id,
                           ServiceID = dat.ServiceID,
                           ServiceName = dat.Service.Name,
                           AreaID = dat.AreaID,
                           IsActive = dat.IsActive
                       }).ToList();

            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    ApplicationLog.Instance.WriteError(String.Format("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State));
                    foreach (var ve in eve.ValidationErrors)
                    {
                        ApplicationLog.Instance.WriteError(String.Format("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage));
                    }
                }
            }
            catch (Exception ex)
            {
                ApplicationLog.Instance.WriteException(ex);
            }
            finally
            {
                if (Entities == null)
                    entities.Dispose();
            }
            return lst;
        }

        public ServiceInAreaInfo GetServiceInArea(int nAreaId, int nServiceId, HappyHutDataBaseEntities Entities = null)
        {
            ServiceInAreaInfo inf = null;
            HappyHutDataBaseEntities entities = null;
            try
            {
                entities = Entities == null ? new HappyHutDataBaseEntities() : Entities;

                var dat = entities.ServicesInAreas.SingleOrDefault(x => x.ServiceID.Equals(nServiceId) && x.AreaID.Equals(nAreaId) && x.IsActive);

                if (dat != null)
                {
                    inf = new ServiceInAreaInfo()
                     {
                         ServiceInAreaId = dat.Id,
                         ServiceID = dat.ServiceID,
                         ServiceName = dat.Service.Name,
                         AreaID = dat.AreaID,
                         IsActive = dat.IsActive
                     };
                }
                else
                    ApplicationLog.Instance.WriteDebug(String.Format("Couldnt find active area in service for id = {0} and area id = {1}", nServiceId, nAreaId));
            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    ApplicationLog.Instance.WriteError(String.Format("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State));
                    foreach (var ve in eve.ValidationErrors)
                    {
                        ApplicationLog.Instance.WriteError(String.Format("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage));
                    }
                }
            }
            catch (Exception ex)
            {
                ApplicationLog.Instance.WriteException(ex);
            }
            finally
            {
                if (Entities == null)
                    entities.Dispose();
            }
            return inf;
        }

        public List<CityInfo> GetCityList(HappyHutDataBaseEntities Entities = null)
        {
            List<CityInfo> lst = new List<CityInfo>();
            HappyHutDataBaseEntities entities = null;
            try
            {
                entities = Entities == null ? new HappyHutDataBaseEntities() : Entities;

                lst = (from dat in entities.Cities
                       select new CityInfo()
                       {
                           ID = dat.ID,
                           Name = dat.Name
                       }).ToList();

            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    ApplicationLog.Instance.WriteError(String.Format("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State));
                    foreach (var ve in eve.ValidationErrors)
                    {
                        ApplicationLog.Instance.WriteError(String.Format("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage));
                    }
                }
            }
            catch (Exception ex)
            {
                ApplicationLog.Instance.WriteException(ex);
            }
            finally
            {
                if (Entities == null)
                    entities.Dispose();
            }
            return lst;
        }

        public List<GetQuoteInfo> GetRequestsToSendEmail(HappyHutDataBaseEntities Entities = null)
        {
            List<GetQuoteInfo> lst = new List<GetQuoteInfo>();
            HappyHutDataBaseEntities entities = null;
            try
            {
                entities = Entities == null ? new HappyHutDataBaseEntities() : Entities;

                var requests = from dat in entities.GetQuoteRequests
                               where !dat.IsEmailSent
                               && !dat.EmailSentDt.HasValue
                               select new GetQuoteInfo()
                               {
                                   Id = dat.Id,
                                   FirstName = dat.FirstName,
                                   LastName = dat.LastName,
                                   Email = dat.Email,
                                   MobileNumber = dat.MobileNumber,
                                   ServiceId = dat.ServiceId,
                                   ServiceName = dat.ServicesInArea.Service.Name,
                                   AdditionalInfo = dat.AdditionalInfo,
                                   NeedToCreateUser = !entities.AspNetUsers.Any(x => x.Email.ToLower().Equals(dat.Email.ToLower())),
                                   PreferredDate = dat.PreferredDate,
                                   PreferredTime = dat.PreferredTime,
                               };
                lst = requests.AsEnumerable().OrderBy(dat => dat.CreateDt).ToList();
            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    ApplicationLog.Instance.WriteError(String.Format("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State));
                    foreach (var ve in eve.ValidationErrors)
                    {
                        ApplicationLog.Instance.WriteError(String.Format("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage));
                    }
                }
            }
            catch (Exception ex)
            {
                ApplicationLog.Instance.WriteException(ex);
            }
            finally
            {
                if (Entities == null)
                    entities.Dispose();
            }
            return lst;
        }

        public bool SetHappyHutUserInfo(HappyHutUserInfo inf, HappyHutDataBaseEntities Entities = null)
        {
            bool bFlag = false;
            DateTime now = DateTime.UtcNow;
            HappyHutDataBaseEntities entities = null;
            try
            {
                entities = Entities == null ? new HappyHutDataBaseEntities() : Entities;
                HappyHutUser dat = entities.HappyHutUsers.SingleOrDefault(x => x.AspNetUser.UserName.ToLower().Equals(inf.Username.ToLower()));

                if (dat == null)
                {
                    dat = new HappyHutUser()
                    {
                        UserId = entities.AspNetUsers.SingleOrDefault(x => x.UserName.ToLower().Equals(inf.Username.ToLower())).Id,
                        CreateDt = now,
                    };
                    entities.HappyHutUsers.Add(dat);
                }

                dat.FirstName = !String.IsNullOrEmpty(inf.FirstName) ? inf.FirstName : String.Empty;
                dat.LastName = !String.IsNullOrEmpty(inf.LastName) ? inf.LastName : String.Empty;
                dat.MobileNumber = !String.IsNullOrEmpty(inf.MobileNumber) ? inf.MobileNumber : String.Empty;
                dat.AddressLine1 = !String.IsNullOrEmpty(inf.AddressLine1) ? inf.AddressLine1 : String.Empty;
                dat.AddressLine2 = !String.IsNullOrEmpty(inf.AddressLine2) ? inf.AddressLine2 : String.Empty;
                dat.CityId = inf.CityId;
                dat.StateId = entities.Cities.SingleOrDefault(x => x.ID.Equals(inf.CityId)).StateId;
                dat.IsFirstLogin = inf.IsFirstLogin;
                dat.LastUpdateDt = now;

                if (Entities == null)
                    entities.SaveChanges();
                bFlag = true;
            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    ApplicationLog.Instance.WriteError(String.Format("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State));
                    foreach (var ve in eve.ValidationErrors)
                    {
                        ApplicationLog.Instance.WriteError(String.Format("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage));
                    }
                }
            }
            catch (Exception ex)
            {
                ApplicationLog.Instance.WriteException(ex);
            }
            finally
            {
                if (Entities == null)
                    entities.Dispose();
            }
            return bFlag;
        }

        public bool SetHappyHutUsersInfos(List<HappyHutUserInfo> infs, HappyHutDataBaseEntities Entities = null)
        {
            bool bFlag = false;
            DateTime now = DateTime.UtcNow;
            HappyHutDataBaseEntities entities = null;
            try
            {
                entities = Entities == null ? new HappyHutDataBaseEntities() : Entities;
                List<bool> lst1 = new List<bool>();
                foreach (HappyHutUserInfo inf in infs)
                {
                    lst1.Add(SetHappyHutUserInfo(inf, entities));
                }

                if (lst1.Count > 0)
                    bFlag = lst1.Any(x => x.Equals(false));
            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    ApplicationLog.Instance.WriteError(String.Format("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State));
                    foreach (var ve in eve.ValidationErrors)
                    {
                        ApplicationLog.Instance.WriteError(String.Format("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage));
                    }
                }
            }
            catch (Exception ex)
            {
                ApplicationLog.Instance.WriteException(ex);
            }
            finally
            {
                if (Entities == null)
                    entities.Dispose();
            }
            return bFlag;
        }

        public List<ServiceInfo> GetServices(HappyHutDataBaseEntities Entities = null)
        {
            List<ServiceInfo> lst = new List<ServiceInfo>();
            HappyHutDataBaseEntities entities = null;
            try
            {
                entities = Entities == null ? new HappyHutDataBaseEntities() : Entities;

                lst = (from dat in entities.Services
                       select new ServiceInfo()
                       {
                           Id = dat.Id,
                           Name = dat.Name
                       }).ToList();
            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    ApplicationLog.Instance.WriteError(String.Format("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State));
                    foreach (var ve in eve.ValidationErrors)
                    {
                        ApplicationLog.Instance.WriteError(String.Format("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage));
                    }
                }
            }
            catch (Exception ex)
            {
                ApplicationLog.Instance.WriteException(ex);
            }
            finally
            {
                if (Entities == null)
                    entities.Dispose();
            }
            return lst;
        }
    }
}
