using AutoMapper;
using MyCBTS.IDP.Login.Models;
using MyCBTS.IDP.Login.Models.Api;
using System;
using System.Collections.Generic;
using ApiClient = MyCBTS.Api.Client;

namespace MyCBTS.IDP.Login.Utility
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<RegisterViewModel, User>()
                .ForMember(u => u.UserName, opt => opt.MapFrom(x => x.EmailAddress))
                .ForMember(u => u.Email, opt => opt.MapFrom(x => x.EmailAddress));

            CreateMap<AccountDetails, RegisterViewModel>()
                .ForMember(u => u.AccountNumber, opt => opt.MapFrom(x => x.accountNumber))
                .ForMember(u => u.BillingSystem, opt => opt.MapFrom(x => x.billingSystem))
                .ForMember(u => u.AccountStatus, opt => opt.MapFrom(x => x.accountStatus));

            CreateMap<ApiClient.UserLogin, User>()
               .ForMember(u => u.UserName, opt => opt.MapFrom(x => x.Email))
               .ForMember(u => u.Password, opt => opt.MapFrom(x => x.Password))
               .ForMember(u => u.BrandName, opt => opt.MapFrom(x => x.ApplicationName));

            CreateMap<ApiClient.Account, Account>();
            CreateMap<Account, ApiClient.Account>();
            CreateMap<Account, ApiClient.UserProfileAccount>();

            CreateMap<ApiClient.User, User>()
                  .ForMember(dest => dest.Accounts, opt => opt.MapFrom(src => src.Accounts));   

            CreateMap<User, ApiClient.User>()
                  .ForMember(dest => dest.Accounts, opt => opt.MapFrom(src => src.Accounts));

            CreateMap<User, ApiClient.UserProfile>();

            CreateMap<ApiClient.CustomerAccountInfo, AccountDetails>();

            CreateMap<ApiClient.CbtsBillSummary,BillSummary>()
                 .ForMember(u => u.BillDate, opt => opt.MapFrom(x =>x.BillDate.DateTime));

            CreateMap<ApiClient.CbadBillSummary, BillSummary>()
                 .ForMember(u => u.BillDate, opt => opt.MapFrom(x => x.BillDate.DateTime));

            CreateMap<ApiClient.CbtsCABillSummary, BillSummary>()
                 .ForMember(u => u.BillDate, opt => opt.MapFrom(x => x.BillDate.DateTime));

            CreateMap<ApiClient.CbtsUKBillSummary, BillSummary>()
                 .ForMember(u => u.BillDate, opt => opt.MapFrom(x => x.BillDate.DateTime));

            CreateMap<ApiClient.CrisBillSummary, BillSummary>()
                 .ForMember(u => u.BillDate, opt => opt.MapFrom(x => x.BillDate.DateTime));

            CreateMap<ApiClient.CabsBillSummary, BillSummary>()
                 .ForMember(u => u.BillDate, opt => opt.MapFrom(x => x.BillDate.DateTime));

            CreateMap<ApiClient.BillSummary, BillSummary>()
               .ForMember(u => u.BillDate, opt => opt.MapFrom(x => x.BillDate.DateTime));

            #region Identity          
            CreateMap<ApiClient.Client, Client>();
            CreateMap<Client, ApiClient.Client>();

            CreateMap<ApiClient.User,User>();
            CreateMap<User, ApiClient.User>();

            CreateMap<ApiClient.RefreshToken, RefreshToken>();
            CreateMap<RefreshToken, ApiClient.RefreshToken>();

            CreateMap<ApiClient.Token,Duende.IdentityServer.Models.Token>();

            CreateMap<Duende.IdentityServer.Models.Token, ApiClient.Token>();

            CreateMap<ApiClient.Token, Duende.IdentityServer.Models.AuthorizationCode>()
              .ForMember(u => u.CodeChallenge, opt => opt.MapFrom(x => x.AuthCodeChallenge))
            .ForMember(u => u.CodeChallengeMethod, opt => opt.MapFrom(x => x.AuthCodeChallengeMethod));
            CreateMap<Duende.IdentityServer.Models.AuthorizationCode, ApiClient.Token>();

            CreateMap<ApiClient.RefreshToken, RefreshToken>()
                 .ForMember(u => u.CreationTime, opt => opt.MapFrom(x => x.CreationTime.DateTime));
            #endregion
        }
    }
}