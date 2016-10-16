using BussinessObjects;
using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Web;
namespace HappyHutService
{
    [ServiceContract]
    interface IHappyHutService
    {
        [OperationContract]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.Wrapped)]
        bool SetCustomerRequestInfo(GetQuoteInfo inf, HappyHutDataBaseEntities Entities = null);

        [OperationContract]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.Wrapped)]
        bool SetCustomerRequestInfos(List<GetQuoteInfo> infs, HappyHutDataBaseEntities Entities = null);

        [OperationContract]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.Wrapped)]
        List<AreaInCityInfo> GetAreasInCity(int cid, HappyHutDataBaseEntities Entities = null);

        [OperationContract]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.Wrapped)]
        List<ServiceInAreaInfo> GetServicesInArea(int areaid, HappyHutDataBaseEntities Entities = null);

        [OperationContract]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.Wrapped)]
        ServiceInAreaInfo GetServiceInArea(int nAreaId, int nServiceId, HappyHutDataBaseEntities Entities = null);

        [OperationContract]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.Wrapped)]
        List<CityInfo> GetCityList(HappyHutDataBaseEntities Entities = null);

        [OperationContract]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.Wrapped)]
        List<GetQuoteInfo> GetRequestsToSendEmail(HappyHutDataBaseEntities Entities = null);

        [OperationContract]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.Wrapped)]
        bool SetHappyHutUserInfo(HappyHutUserInfo inf, HappyHutDataBaseEntities Entities = null);

        [OperationContract]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.Wrapped)]
        bool SetHappyHutUsersInfos(List<HappyHutUserInfo> infs, HappyHutDataBaseEntities Entities = null);

        [OperationContract]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.Wrapped)]
        List<ServiceInfo> GetServices(HappyHutDataBaseEntities Entities = null);
    }
}
