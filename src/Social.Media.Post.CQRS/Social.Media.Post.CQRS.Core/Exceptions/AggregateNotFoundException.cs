namespace Social.Media.Post.CQRS.Core.Exceptions;

public class AggregateNotFoundException(string message) : Exception(message);