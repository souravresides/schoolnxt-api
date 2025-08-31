namespace SchoolNexAPI.Enums
{
    public enum BlobEntity
    {
        Schools,
        Students,
        Teachers,
        Classes,
        Finance,
        Platform, // special case: global, no schoolId
        Users
    }

    public enum BlobCategory
    {
        Profile,
        ProfilePictures,
        Assignments,
        Reports,
        Invoices,
        Policies,
        Documents
    }

}
