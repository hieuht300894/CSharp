using System;

namespace OnlineShop.Models
{
    public interface IEF
    {
        Int32 KeyID { get; set; }
        String Note { get; set; }
        Int32 Status { get; set; }
        Boolean IsDefault { get; set; }
    }
    public interface IMaster
    {
        String Code { get; set; }
        String Name { get; set; }
        DateTime CreatedDate { get; set; }
        Int32 CreatedBy { get; set; }
        DateTime? ModifiedDate { get; set; }
        Int32? ModifiedBy { get; set; }
    }
    public interface IProduct
    {
        Int32 IDProduct { get; set; }
        String CodeProduct { get; set; }
        String NameProduct { get; set; }
    }
    public interface IUnit
    {
        Int32 IDUnit { get; set; }
        String CodeUnit { get; set; }
        String NameUnit { get; set; }
    }
    public interface ICustomer
    {
        Int32 IDCustomer { get; set; }
        String CodeCustomer { get; set; }
        String NameCustomer { get; set; }
    }
    public interface IWarehouse
    {
        Int32 IDWarehouse { get; set; }
        String CodeWarehouse { get; set; }
        String NameWarehouse { get; set; }
    }
    public interface IProvider
    {
        Int32 IDProvider { get; set; }
        String CodeProvider { get; set; }
        String NameProvider { get; set; }
    }
    public interface IProductCategory
    {
        Int32 IDProductCategory { get; set; }
        String CodeProductCategory { get; set; }
        String NameProductCategory { get; set; }
    }
    public interface ICurrency
    {
        Int32 IDCurrency { get; set; }
        String CodeCurrency { get; set; }
        String NameCurrency { get; set; }
    }
    public interface ICountry
    {
        Int32 IDCountry { get; set; }
        String CodeCountry { get; set; }
        String NameCountry { get; set; }
    }
    public interface IAccount
    {
        Int32 IDAccount { get; set; }
        String CodeAccount { get; set; }
        String NameAccount { get; set; }
    }
    public interface IAgency
    {
        Int32 IDAgency { get; set; }
        String CodeAgency { get; set; }
        String NameAgency { get; set; }
    }
    public interface IFunction
    {
        Int32 IDFunction { get; set; }
        String CodeFunction { get; set; }
        String NameFunction { get; set; }
    }
    public interface IPermission
    {
        Int32 IDPermission { get; set; }
        String Controller { get; set; }
        String Action { get; set; }
        String Method { get; set; }
        String Template { get; set; }
        String Path { get; set; }
    }
    public interface IPermissionCategory
    {
        Int32 IDPermissionCategory { get; set; }
        String CodePermissionCategory { get; set; }
        String NamePermissionCategory { get; set; }
    }
    public interface IPersonnel
    {
        Int32 IDPersonnel { get; set; }
        String CodePersonnel { get; set; }
        String NamePersonnel { get; set; }
    }
    public interface IPermissionDetail
    {
        Int32 IDPermissionDetail { get; set; }
        String CodePermissionDetail { get; set; }
        String NamePermissionDetail { get; set; }
    }
    public interface IImportProductProvider
    {
        Int32 IDImportProductProvider { get; set; }
        String CodeImportProductProvider { get; set; }
        String NameImportProductProvider { get; set; }
    }
    public interface IType
    {
        Int32 IDType { get; set; }
        String CodeType { get; set; }
        String NameType { get; set; }
    }
    public interface IGender
    {
        Int32 IDGender { get; set; }
        String CodeGender { get; set; }
        String NameGender { get; set; }
    }
}
