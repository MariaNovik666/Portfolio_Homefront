public interface IResource
{
    string Type { get; }
    int HP { get; set; }
    void Hit(int damage)
    {
        HP -= damage;
    }
}