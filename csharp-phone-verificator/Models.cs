using System;

namespace PhoneVerificator
{
  public class PhoneVerificationLogEntry
  {
    public long Id { get; set; }
    public string PhoneNumber { get; set; }
    public DateTime VerifiedTime { get; set; }
  }
}
