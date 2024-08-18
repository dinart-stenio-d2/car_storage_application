namespace Car.Storage.Application.SharedKernel.DomainObjects
{
    public abstract class Entity
    {
        public Guid Id { get; set; }

        protected Entity(string idParam = null)
        {
            if (string.IsNullOrEmpty(idParam))
            {
                Id = Guid.NewGuid();
            }
            else
            {
                Id = new Guid(idParam);
            }
        }

        /// <summary>
        /// Method used to compare instances of a class
        /// Each entity have own identity so for one entity
        /// to be equal to another it need to be of the same type 
        /// and also have the same Id
        /// </summary>
        /// <param name="obj"></param>
        /// <returns>true or false</returns>
        public override bool Equals(object? obj)
        {
            var compareTo = obj as Entity;

            if (ReferenceEquals(this, compareTo)) return true;
            if (ReferenceEquals(null, compareTo)) return false;

            return Id.Equals(compareTo.Id);
        }

    }
}
