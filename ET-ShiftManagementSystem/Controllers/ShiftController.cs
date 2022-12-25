﻿using AutoMapper;
using ET_ShiftManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;
using ShiftManagementServises.Servises;
using ShiftMgtDbContext.Entities;

namespace ET_ShiftManagementSystem.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class ShiftController : Controller
    {
        private readonly IShiftServices shiftServices;
        private readonly IMapper mapper;

        public ShiftController(IShiftServices shiftServices , IMapper mapper)
        {
            this.shiftServices = shiftServices;
            this.mapper = mapper;
        }

        [HttpGet]
        [Route("All/")]
        public async Task<IActionResult> GetAllShifts()
        {
            var shift =  await shiftServices.GetAllShiftAsync();

            var ShiftDTO = mapper.Map<List<Models.ShiftDTO>>(shift);

            return Ok(ShiftDTO);
        }

        [HttpGet]
        [Route("single/")]
        public async Task<IActionResult> GetShiftByID(int id)
        {
            var shift = await shiftServices.GetShiftById(id);

            var ShiftDTO = mapper.Map<Models.ShiftDTO>(shift);

            return Ok(ShiftDTO);
        }

        [HttpPost]

        public IActionResult AddShift(Shift shiftDTO)
        {
            if (shiftDTO == null)
            {
                return BadRequest();
            }
            var shift = new Shift()
            {
                ShiftName = shiftDTO.ShiftName,
                StartTime    = DateTime.Now,
                EndTime = DateTime.Now,

            };
            shiftServices.AddSift(shift);

            return Ok(shift);

        }

        [HttpPut]

        public async Task<IActionResult> UpdateShift(int id,Models.UpdateShiftRequest shiftDTO)
        {
            var shift = new ShiftMgtDbContext.Entities.Shift()
            {
                ShiftId = shiftDTO.ShiftId,
                ShiftName = shiftDTO.ShiftName,
                StartTime = DateTime.Now,
                EndTime = DateTime.Now
                
            };

            await shiftServices.UpdateShiftAsync(id, shift);

            return Ok(shift);
        }

        [HttpDelete]

        public async Task<IActionResult> DeleteShiftAsync(int Id)
        {
            var delete = await shiftServices.DeleteShiftAsync(Id);

            if (delete == null)
            {
                return NotFound();
            }

            var DeleteDTO = new Models.ShiftDTO()
            {

                ShiftId  = delete.ShiftId,
                ShiftName = delete.ShiftName,
                StartTime = delete.StartTime,
                EndTime = delete.EndTime,
                
            };

            return Ok(DeleteDTO);
        
        }
    }
}