using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ConnectUs.Entity.Dto.request
{
    public record AddressUpdateRequestDTO(
          [Required] String token,
          [Required] long adressId,
          String description,
          String value
  );
    }
    
