using System.ComponentModel.DataAnnotations;

namespace donatETH.Models
{
    public class Post: IValidatableObject
    {

        public string Id { get; set; } = "";
        [Display(Name = "Title")]
        public string Title { get; set; }
        [Display(Name = "Description")]
        public string Text { get; set; }
        [Display(Name = "Image URL")]
        [Url]
        public string ImageUrl { get; set; }
        [Display(Name = "Fundraiser")]
        public string Creator { get; set; }
        [Display(Name = "Price of donate (in Gwei)")]
        public int Price { get; set; }
        [Display(Name = "Goal amount (in Gwei)")]
        public int Goal { get; set; }

        public int CurrentSum { get; set; }
        public int LikeCount { get; set; }

        public int Index { get; set; } = 0;

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> errors = new List<ValidationResult>();

            if (string.IsNullOrWhiteSpace(this.Title))
            {
                errors.Add(new ValidationResult("Title is required"));
            }
            if (string.IsNullOrWhiteSpace(this.Text))
            {
                errors.Add(new ValidationResult("Description is required"));
            }
            if (string.IsNullOrWhiteSpace(this.ImageUrl))
            {
                errors.Add(new ValidationResult("Image is required. Find any suitable image and copy its URL."));
            }
            if (this.Price == null)
            {
                errors.Add(new ValidationResult("Price is required"));
            }
            if(this.Goal < this.Price)
            {
                errors.Add(new ValidationResult("Your price is bigger than goal amount"));
            }
            if (this.Goal%this.Price!=0)
            {
                errors.Add(new ValidationResult("Your goal amount should be multiple of the price"));
            }

            return errors;
        }

    }
}
