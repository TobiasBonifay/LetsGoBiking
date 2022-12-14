//------------------------------------------------------------------------------
// <auto-generated>
//     Ce code a été généré par un outil.
//     Version du runtime :4.0.30319.42000
//
//     Les modifications apportées à ce fichier peuvent provoquer un comportement incorrect et seront perdues si
//     le code est régénéré.
// </auto-generated>
//------------------------------------------------------------------------------

namespace RoutingServer.ProxyService {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="ProxyService.IProxyService")]
    public interface IProxyService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IProxyService/GetAllContracts", ReplyAction="http://tempuri.org/IProxyService/GetAllContractsResponse")]
        string GetAllContracts();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IProxyService/GetAllContracts", ReplyAction="http://tempuri.org/IProxyService/GetAllContractsResponse")]
        System.Threading.Tasks.Task<string> GetAllContractsAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IProxyService/GetAllStations", ReplyAction="http://tempuri.org/IProxyService/GetAllStationsResponse")]
        string GetAllStations();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IProxyService/GetAllStations", ReplyAction="http://tempuri.org/IProxyService/GetAllStationsResponse")]
        System.Threading.Tasks.Task<string> GetAllStationsAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IProxyService/GetStation", ReplyAction="http://tempuri.org/IProxyService/GetStationResponse")]
        string GetStation(string name);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IProxyService/GetStation", ReplyAction="http://tempuri.org/IProxyService/GetStationResponse")]
        System.Threading.Tasks.Task<string> GetStationAsync(string name);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IProxyService/GetStationByContract", ReplyAction="http://tempuri.org/IProxyService/GetStationByContractResponse")]
        string GetStationByContract(string contract);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IProxyService/GetStationByContract", ReplyAction="http://tempuri.org/IProxyService/GetStationByContractResponse")]
        System.Threading.Tasks.Task<string> GetStationByContractAsync(string contract);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IProxyServiceChannel : RoutingServer.ProxyService.IProxyService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class ProxyServiceClient : System.ServiceModel.ClientBase<RoutingServer.ProxyService.IProxyService>, RoutingServer.ProxyService.IProxyService {
        
        public ProxyServiceClient() {
        }
        
        public ProxyServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public ProxyServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public ProxyServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public ProxyServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public string GetAllContracts() {
            return base.Channel.GetAllContracts();
        }
        
        public System.Threading.Tasks.Task<string> GetAllContractsAsync() {
            return base.Channel.GetAllContractsAsync();
        }
        
        public string GetAllStations() {
            return base.Channel.GetAllStations();
        }
        
        public System.Threading.Tasks.Task<string> GetAllStationsAsync() {
            return base.Channel.GetAllStationsAsync();
        }
        
        public string GetStation(string name) {
            return base.Channel.GetStation(name);
        }
        
        public System.Threading.Tasks.Task<string> GetStationAsync(string name) {
            return base.Channel.GetStationAsync(name);
        }
        
        public string GetStationByContract(string contract) {
            return base.Channel.GetStationByContract(contract);
        }
        
        public System.Threading.Tasks.Task<string> GetStationByContractAsync(string contract) {
            return base.Channel.GetStationByContractAsync(contract);
        }
    }
}
