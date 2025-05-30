using UnityEngine;

namespace Game.Input
{
    public class PlayerInput : MonoBehaviour, IInputProvider
    {
        private PlayerControls controls;

        private Vector2 _move;
        private bool _punchPressed;
        private bool _kickPressed;
        private bool _powerPressed;
        private bool _jumpPressed;
        private bool _strikePressed;

        /* -------------------- IInputProvider properties -------------------- */
        public Vector2 MoveVector => _move;
        public bool PunchPressed => _punchPressed;
        public bool KickPressed => _kickPressed;
        public bool PowerPressed => _powerPressed;
        public bool StrikePressed => _strikePressed;
        public bool JumpPressed => _jumpPressed;

        private void Awake()
        {
            controls = new PlayerControls();
            controls.Enable();
        }

        private void OnDestroy()
        {
            controls.Disable();
        }

        private void Update()
        {
            _move = controls.Movement.Move.ReadValue<Vector2>();

            _punchPressed = controls.Combat.Punch.WasPressedThisFrame();
            _kickPressed = controls.Combat.Kick.WasPressedThisFrame();
            _powerPressed = controls.Combat.Power.WasPressedThisFrame();
            _strikePressed = controls.Combat.Strike.WasPressedThisFrame();
            _jumpPressed = controls.Movement.Jump.WasPressedThisFrame();

        }
    }
}
