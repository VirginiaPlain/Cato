using Microsoft.ProjectOxford.Face;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cato.Shared.FaceApi
{
    /// <summary>
    /// Wrapper around the MS FaceApi FaceServiceClient
    /// </summary>
    public class FaceApiClient
    {

        private readonly IFaceServiceClient _faceServiceClient;

        private const string PersonGroupId = "alexgrp";


        public async Task AddPersonFace(string personId, string faceId)
        {
            Guid personGuid = Guid.Parse(personId);
            Guid faceGuid = Guid.Parse(faceId);
            //Task.Run(() => _faceServiceClient.AddPersonFaceAsync(PersonGroupId, personGuid, faceGuid));
            await _faceServiceClient.AddPersonFaceAsync(PersonGroupId, personGuid, string.Empty);

        }

    }
}
