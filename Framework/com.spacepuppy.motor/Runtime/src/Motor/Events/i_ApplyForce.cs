﻿using UnityEngine;
using System.Collections.Generic;

using com.spacepuppy;
using com.spacepuppy.Events;
using com.spacepuppy.Motor;
using com.spacepuppy.Utils;

namespace com.spacepuppy.Motor.Events
{

    public class i_ApplyForce : Triggerable
    {

        #region Fields

        [SerializeField()]
        [UnityEngine.Serialization.FormerlySerializedAs("TargetObject")]
        [TriggerableTargetObject.Config(typeof(GameObject))]
        private TriggerableTargetObject _target = new TriggerableTargetObject();
        [SerializeField()]
        [UnityEngine.Serialization.FormerlySerializedAs("Force")]
        private ConfigurableForce _force = new ConfigurableForce();
        [SerializeField()]
        private bool _targetEntireEntity = true;

        #endregion

        #region Properties

        public TriggerableTargetObject Target
        {
            get { return _target; }
        }

        public ConfigurableForce Force
        {
            get { return _force; }
        }

        public bool TargetEntireEntity
        {
            get { return _targetEntireEntity; }
            set { _targetEntireEntity = value; }
        }

        #endregion

        #region TriggerableMechanism Interface

        public override bool Trigger(object sender, object arg)
        {
            if (!this.CanTrigger) return false;

            var targ = _target.GetTarget<GameObject>(arg);
            if (targ == null) return false;
            if (_targetEntireEntity) targ = GameObjectUtil.FindRoot(targ);


            IMotor controller;
            if (targ.GetComponentInChildren<IMotor>(out controller))
            {
                //controller.AddForce(this.Force.GetForce(this.transform), this.Force.ForceMode);
                this.Force.ApplyForce(this.transform, controller);
                return true;
            }
            Rigidbody body;
            if (targ.GetComponentInChildren<Rigidbody>(out body))
            {
                this.Force.ApplyForce(this.transform, body);
                return true;
            }

            return false;
        }

        #endregion

    }

}