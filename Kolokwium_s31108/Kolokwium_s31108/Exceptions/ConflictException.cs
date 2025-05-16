namespace Kolokwium_s31108.Exceptions;

public class ConflictException : Exception
{
    public ConflictException() : base() { }
    public ConflictException(string message) : base(message) { }
}