using Domain.Core;
using Service.Interfaces;
using sstu_nevdev.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace sstu_nevdev.Controllers
{
    public class CheckpointsController : ApiController
    {
        ICheckpointService checkpointService;
        ICheckpointAdmissionService admissionCheckpointService;

        public CheckpointsController(ICheckpointService checkpointService, ITypeService typeService, IAdmissionService admissionService, ICheckpointAdmissionService admissionCheckpointService)
        {
            this.checkpointService = checkpointService;
            this.admissionCheckpointService = admissionCheckpointService;
        }

        // GET api/checkpoints
        public IEnumerable<Checkpoint> Get()
        {
            return checkpointService.GetAll();
        }

        // GET api/checkpoints/5
        public Checkpoint Get(int id)
        {
            return checkpointService.Get(id);
        }

        // POST api/checkpoints
        [HttpPost]
        public void Post([FromBody]CheckpointModel item)
        {
            checkpointService.Create(new Checkpoint
            {
                Campus = item.Campus,
                CreatedBy = item.CreatedBy,
                Description = item.Description,
                Row = item.Row,
                Status = item.Status,
                TypeID = item.TypeID,
                UpdatedBy = item.UpdatedBy
            });
            int checkpointID = checkpointService.GetAll().Where(d => d.Description == item.Description).Last().ID;
            foreach (var i in item.Admissions)
            {
                admissionCheckpointService.Create(new CheckpointAdmission { AdmissionID = i.ID, CheckpointID = checkpointID });
            }
        }

        // PUT api/checkpoints/5
        [HttpPut]
        public void Put(int id, [FromBody]CheckpointModel item)
        {
            if (id == item.ID)
            {
                Checkpoint checkpoint = checkpointService.Get(item.ID);
                if (item.Row != 0)
                {
                    checkpoint.Row = item.Row;
                }
                if (item.Status != null)
                {
                    checkpoint.Status = item.Status;
                }
                if (item.Campus != 0)
                {
                    checkpoint.Campus = item.Campus;
                }
                if (item.CreatedBy != null)
                {
                    checkpoint.CreatedBy = item.CreatedBy;
                }
                if (item.Description != null)
                {
                    checkpoint.Description = item.Description;
                }
                if (item.TypeID != 0)
                {
                    checkpoint.TypeID = item.TypeID;
                }
                if (item.UpdatedBy != null)
                {
                    checkpoint.UpdatedBy = item.UpdatedBy;
                }
                checkpointService.Edit(checkpoint);

                foreach (var i in item.Admissions)
                {
                    if (i.IsChecked)
                    {
                        if (!admissionCheckpointService.IsMatch(item.ID, i.ID))
                        {
                            admissionCheckpointService.Create(new CheckpointAdmission { AdmissionID = i.ID, CheckpointID = item.ID });
                        }
                    }
                    else
                    {
                        if (admissionCheckpointService.IsMatch(item.ID, i.ID))
                        {

                            CheckpointAdmission tryToDelete = admissionCheckpointService.GetAll().Where(c => (c.CheckpointID == item.ID) && (c.AdmissionID == i.ID)).FirstOrDefault();
                            admissionCheckpointService.Delete(tryToDelete);
                        }
                    }
                }
            }
        }

        // DELETE api/checkpoints/5
        [HttpDelete]
        public void Delete(int id)
        {
            Checkpoint item = checkpointService.Get(id);
            if (item != null)
            {
                checkpointService.Delete(item);
            }
        }
    }
}