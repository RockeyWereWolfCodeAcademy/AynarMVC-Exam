﻿namespace AynarMVC_Exam.Exceptions
{
    public class RolesCreationFailedException : Exception
    {
        public RolesCreationFailedException()
        {
        }

        public RolesCreationFailedException(string? message) : base(message)
        {
        }
    }
}
