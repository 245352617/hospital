using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HisApiMockService.Models;
[Table("BuildPatient")]
public class CreatePatientResponseDto
{
    [Key]
    public int Id { get; set; }
    public string? PatientId
    {
        get;
        set;
    }

    public string? PatientName
    {
        get;
        set;
    }

    public string? Py
    {
        get;
        set;
    }

    public string? Sex
    {
        get;
        set;
    }

    public string? IdentityNo
    {
        get;
        set;
    }

    public DateTime? Birthday
    {
        get;
        set;
    }

    public string? ContactsPhone
    {
        get;
        set;
    }

    public string? ChargeType
    {
        get;
        set;
    }

    public string? Address
    {
        get;
        set;
    }

    public string? Nation
    {
        get;
        set;
    }

    public string? Country
    {
        get;
        set;
    }

    public string? CountryCode
    {
        get;
        set;
    }

    public string? CardNo
    {
        get;
        set;
    }

    public string? Age
    {
        get;
        set;
    }

    public string? ErrorMsg
    {
        get;
        set;
    }

    public DateTime? StartTriageTime
    {
        get;
        set;
    }
}
