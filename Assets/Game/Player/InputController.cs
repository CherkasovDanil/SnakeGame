using UniRx;
using UnityEngine;
using Zenject;

namespace Game.Player
{
    public readonly struct OnChangeDirectionMessage
    {
        public readonly Direction MessageDirection;

        public OnChangeDirectionMessage(Direction direction)
        {
            MessageDirection = direction;
        }
    }

    public readonly struct OnStartTrackInputMessage
    {
        public readonly bool TrackInput;

        public OnStartTrackInputMessage(bool trackInput)
        {
            TrackInput = trackInput;
        }
    }
    
    public class InputController : ITickable
    {
        private Direction _cashedDirection;
        
        private bool _trackInput = true;

        public InputController(SnakeConfig snakeConfig)
        {
            _cashedDirection = snakeConfig.StartDirection;

            MessageBroker
                .Default
                .Receive<OnStartTrackInputMessage>()
                .Subscribe(message =>
                {
                    _trackInput = message.TrackInput;
                });
        }

        public void Tick()
        {
            if (!_trackInput)
            {
                return;
            }
            
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                _cashedDirection = _cashedDirection switch {
                    Direction.Left => Direction.Down,
                    Direction.Up => Direction.Left,
                    Direction.Right => Direction.Up,
                    Direction.Down => Direction.Right,
                    _ => _cashedDirection
                };
               
                BlockMovingAndUpdateDirection();
            }

            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                _cashedDirection = _cashedDirection switch
                {
                    Direction.Left => Direction.Up,
                    Direction.Up => Direction.Right,
                    Direction.Right => Direction.Down,
                    Direction.Down => Direction.Left,
                    _ => _cashedDirection
                };
                
                BlockMovingAndUpdateDirection();
            }
        }

        private void BlockMovingAndUpdateDirection()
        {
            MessageBroker
                .Default
                .Publish(new OnStartTrackInputMessage(false));
               
            MessageBroker
                .Default
                .Publish(new OnChangeDirectionMessage(_cashedDirection));
        }
    }
}