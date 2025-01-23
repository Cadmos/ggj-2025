using UnityEngine;

namespace GGJ.IoniExtensions
{
    /// <summary>
    /// Extension methods for ParticleSystems
    /// </summary>
    public static class ParticleSystemExtensions 
    {
        ///<summary>
        ///Enables or disables the emission of this ParticleSystem.
        ///</summary>
        ///<param name="particleSystem">The ParticleSystem.</param>
        ///<param name="enabled">Whether to enable or disable emission.</param>
        ///<remarks>
        ///This method directly enables or disables the emission module of the ParticleSystem. 
        ///Use this method when you want to dynamically control whether a ParticleSystem is emitting particles.
        ///</remarks>
        ///<example>
        ///This is an example of how to use the EnableEmission() method:
        ///<code>
        /// ParticleSystem ps = GetComponent<ParticleSystem>();
        /// ps.EnableEmission(false);
        /// </code>
        ///In this example, 'ps' will have its emissions turned off.
        ///</example>
        public static void EnableEmission(this ParticleSystem particleSystem, bool enabled)
        {
            var emission = particleSystem.emission;
            emission.enabled = enabled;
        }

        ///<summary>
        ///Gets the maximum constant rate at which this ParticleSystem is emitting particles over time.
        ///</summary>
        ///<param name="particleSystem">The ParticleSystem.</param>
        ///<returns>The maximum constant emission rate of the ParticleSystem over time.</returns>
        ///<remarks>
        ///This method retrieves the "constantMax" property of the ParticleSystem's emission rate over time, which represents the maximum amount of particles being emitted per time unit.
        ///Use this method when you need to know the emission rate of a ParticleSystem for your gameplay logic or effects balancing.
        ///</remarks>
        ///<example>
        ///This is an example of how to use the GetEmissionRateOverTime() method:
        ///<code>
        /// ParticleSystem ps = GetComponent<ParticleSystem>();
        /// float emissionRate = ps.GetEmissionRateOverTime();
        /// </code>
        ///In this example, 'emissionRate' will hold the maximum constant emission rate over time of 'ps'.
        ///</example>
        public static float GetEmissionRateOverTime(this ParticleSystem particleSystem)
        {
            return particleSystem.emission.rateOverTime.constantMax;
        }

        ///<summary>
        ///Sets the maximum constant rate at which this ParticleSystem should emit particles over time.
        ///</summary>
        ///<param name="particleSystem">The ParticleSystem.</param>
        ///<param name="emissionRate">The new maximum constant emission rate.</param>
        ///<remarks>
        ///This method changes the "constantMax" property of the ParticleSystem's emission rate over time to the provided value.
        ///Use this method when you want to dynamically control the emission rate of a ParticleSystem.
        ///</remarks>
        ///<example>
        ///This is an example of how to use the SetEmissionRateOverTime() method:
        ///<code>
        /// ParticleSystem ps = GetComponent<ParticleSystem>();
        /// ps.SetEmissionRateOverTime(50);
        /// </code>
        ///In this example, 'ps' will have its maximum constant emission rate over time set to 50 particles per time unit.
        ///</example>
        public static void SetEmissionRateOverTime(this ParticleSystem particleSystem, float emissionRate)
        {
            var emission = particleSystem.emission;
            var rate = emission.rateOverTime;
            rate.constantMax = emissionRate;
            emission.rateOverTime = rate;
        }
        
        ///<summary>
        ///Gets the maximum constant rate at which this ParticleSystem is emitting particles over distance.
        ///</summary>
        ///<param name="particleSystem">The ParticleSystem.</param>
        ///<returns>The maximum constant emission rate of the ParticleSystem over distance.</returns>
        ///<remarks>
        ///This method retrieves the "constantMax" property of the ParticleSystem's emission rate over distance, which represents the maximum number of particles being emitted per unit of distance.
        ///Use this method when you need to know the emission rate of a ParticleSystem for your gameplay logic or effects balancing.
        ///</remarks>
        ///<example>
        ///This is an example of how to use the GetEmissionRateOverDistance() method:
        ///<code>
        /// ParticleSystem ps = GetComponent<ParticleSystem>();
        /// float emissionRate = ps.GetEmissionRateOverDistance();
        /// </code>
        ///In this example, 'emissionRate' will hold the maximum constant emission rate over distance of 'ps'.
        ///</example>
        public static float GetEmissionRateOverDistance(this ParticleSystem particleSystem)
        {
            return particleSystem.emission.rateOverDistance.constantMax;
        }

        ///<summary>
        ///Sets the maximum constant rate at which this ParticleSystem should emit particles over distance.
        ///</summary>
        ///<param name="particleSystem">The ParticleSystem.</param>
        ///<param name="emissionRate">The new maximum constant emission rate.</param>
        ///<remarks>
        ///This method alters the "constantMax" property of the ParticleSystem's emission rate over distance to the supplied value.
        ///Use this technique when you need to dynamically adjust the emission rate of a ParticleSystem.
        ///</remarks>
        ///<example>
        ///This is an example of how to use the SetEmissionRateOverDistance method:
        ///<code>
        /// ParticleSystem ps = GetComponent<ParticleSystem>();
        /// ps.SetEmissionRateOverDistance(50);
        /// </code>
        ///In this example, 'ps' will have its maximum constant emission rate over distance set to 50 particles per distance unit.
        ///</example>
        public static void SetEmissionRateOverDistance(this ParticleSystem particleSystem, float emissionRate)
        {
            var emission = particleSystem.emission;
            var rate = emission.rateOverDistance;
            rate.constantMax = emissionRate;
            emission.rateOverDistance = rate;
        }
    }
}
