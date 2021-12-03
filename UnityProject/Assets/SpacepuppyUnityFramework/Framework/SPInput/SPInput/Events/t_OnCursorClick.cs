using UnityEngine;
using System.Collections.Generic;
using System.Linq;

using com.spacepuppy;
using com.spacepuppy.Events;
using com.spacepuppy.Utils;

namespace com.spacepuppy.SPInput.Events
{

    [Infobox("Requires a CursorInputLogic to be configured. This is usually part of the InputManager.")]
    public class t_OnCursorClick : TriggerComponent, CursorInputLogic.IClickHandler
    {

        #region Fields

        [SerializeField]
        [Tooltip("Populate with the Id of the CursorFilterLogic if you want to filter for only a specific input. Otherwise leave blank to receive all clicks.")]
        private string _cursorInputLogicFilter;

        [SerializeField]
        [Tooltip("If CursorInputLogic is configure to dispatch OnClick always, this allows you to ignore it if it was a double click.")]
        private bool _ignoreDoubleClick;

        #endregion

        #region Properties

        public string CursorInputLogicFilter
        {
            get => _cursorInputLogicFilter;
            set => _cursorInputLogicFilter = value;
        }

        public bool IgnoreDoubleClick
        {
            get => _ignoreDoubleClick;
            set => _ignoreDoubleClick = value;
        }

        #endregion

        #region IClickHandler Interface

        void CursorInputLogic.IClickHandler.OnClick(CursorInputLogic sender, Collider c)
        {
            if (_ignoreDoubleClick && (sender?.LastClickWasDoubleClick ?? false)) return;
            if (!string.IsNullOrEmpty(_cursorInputLogicFilter) && sender?.Id != _cursorInputLogicFilter) return;

            this.ActivateTrigger();
        }

        #endregion

    }

}