namespace AbilitySystem
{
    public class AbilitySpec
    {
        private AbilitySystemBehaviour _owner;
        private AbilityScriptableObject _abilityScriptableObject;

        public virtual void Init(AbilitySystemBehaviour owner, AbilityScriptableObject abilityScriptableObject)
        {
            _abilityScriptableObject = abilityScriptableObject;
            _owner = owner;
        }
    }
}