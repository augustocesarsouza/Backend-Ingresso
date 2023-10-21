﻿namespace Ingresso.Domain.Entities
{
    public class UserPermission
    {
        public int Id { get; private set; }

        public int? UserId { get; private set; }
        public User? User { get; private set; }
        public int? PermissionId { get; private set; }
        public Permission? Permission { get; private set; }

        public UserPermission()
        {
        }

        public UserPermission(int id, int? userId, Permission permission)
        {
            Id = id;
            UserId = userId;
            Permission = permission;
        }

        //public UserPermission(int userId, int permissionId)
        //{
        //    Validator(userId, permissionId);
        //}

        //public void Validator(int userId, int permissionId)
        //{
        //    DomainValidationException.When(userId <= 0, "Id do Usuario Should be informed");
        //    DomainValidationException.When(permissionId <= 0, "Id da Permissão Should be informed");

        //    UserId = userId;
        //    PermissionId = permissionId;
        //}
    }
}
