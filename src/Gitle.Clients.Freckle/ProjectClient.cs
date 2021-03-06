﻿namespace Gitle.Clients.Freckle
{
    using System.Collections.Generic;
    using System.Linq;
    using Interfaces;
    using Models;
    using ServiceStack.ServiceClient.Web;


    public class ProjectClient : IProjectClient 
    {
        private readonly JsonServiceClient _client;

        public ProjectClient(string freckleApi, string token)
        {
            _client = new JsonServiceClient(freckleApi);
            _client.Headers.Add("X-FreckleToken", token);
        }

        public List<Project> List()
        {
            return _client.Get<List<ProjectResult>>("projects").Select(result => result.Project).ToList();
        }

        public Project Show(int id)
        {
            return _client.Get<ProjectResult>("projects/" + id).Project;
        }

        protected class ProjectResult
        {
            public Project Project { get; set; }
        }

    }
}
