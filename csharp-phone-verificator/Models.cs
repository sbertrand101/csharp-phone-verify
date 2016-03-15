using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace PhoneVerificator
{
  public class PhoneVerificationLogEntry
  {
    public long Id { get; set; }
    [Index]
    public string PhoneNumber { get; set; }
    public string Code { get; set; }
    [Index]
    public DateTime? VerifiedTime { get; set; }
  }
}
