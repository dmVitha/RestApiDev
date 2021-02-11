using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Assignment.Enums
{
    public enum Module
    {
        [Description("Image processing module")]
        IMAGE_PROCESSING =1,

        [Description("Voice recognition module")]
        VOICE_REC ,

        [Description("Face detection module")]
        FACE_DETECT

    }
}
