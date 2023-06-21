﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using FrogExhibitionDAL.ValidationAttributes;
using FrogExhibitionPL.AppConsts.FrogConstants;
using FrogExhibitionDAL.Enums;

namespace FrogExhibitionBLL.ViewModels.FrogViewModels
{
    public class FrogDetailViewModel
    {
        public Guid Id { get; set; }
        [Required]
        [MinLength(FrogConstraintConstants.MinStrLength)]
        [DefaultValue(FrogDefaultValueConstants.FrogGenusDefaultValue)]
        public string Genus { get; set; }

        [Required]
        [MinLength(FrogConstraintConstants.MinStrLength)]
        [DefaultValue(FrogDefaultValueConstants.FrogSpeciesDefaultValue)]
        public string Species { get; set; }

        [Required]
        [MinLength(FrogConstraintConstants.MinStrLength)]
        [DefaultValue(FrogDefaultValueConstants.FrogColorDefaultValue)]
        public string Color { get; set; }

        [Required]
        [MinLength(FrogConstraintConstants.MinStrLength)]
        [DefaultValue(FrogDefaultValueConstants.FrogHabitatDefaultValue)]
        public string Habitat { get; set; }

        [Required]
        [DefaultValue(FrogDefaultValueConstants.FrogPosisonousDefaultValue)]
        public bool Poisonous { get; set; }

        [Required]
        //[ValidStrings(new string[] { "Male", "Female", "Hermaphrodite" }, ErrorMessage = "Valid options: Male, Female, Hermaphrodite")]
        [DefaultValue(FrogDefaultValueConstants.FrogSexDefaultValue)]
        public FrogSex Sex { get; set; }

        [Required]
        [DefaultValue(FrogDefaultValueConstants.FrogHouseKeepableDefaultValue)]
        public bool HouseKeepable { get; set; }

        [Required]
        [Range(FrogConstraintConstants.MinFrogSize, FrogConstraintConstants.MaxFrogSize)]
        [DefaultValue(FrogDefaultValueConstants.FrogSizeDefaultValue)]
        public float Size { get; set; }

        [Required]
        [Range(FrogConstraintConstants.MinFrogWeight, FrogConstraintConstants.MaxFrogWeight)]
        [DefaultValue(FrogDefaultValueConstants.FrogWeightDefaultValue)]
        public float Weight { get; set; }

        [Required]
        [Range(FrogConstraintConstants.MinFrogAge, FrogConstraintConstants.MaxFrogAge)]
        [DefaultValue(FrogConstraintConstants.MinStrLength)]
        public int CurrentAge { get; set; }
        [Required]
        [Range(FrogConstraintConstants.MinFrogAge,FrogConstraintConstants.MaxFrogAge)]
        [DefaultValue(FrogDefaultValueConstants.FrogMaxAgeDefaultValue)]
        public int MaxAge { get; set; }
        public string Diet { get; set; }
        public string Features { get; set; }
        public List<string> PhotoPaths { get; set; }
    }
}