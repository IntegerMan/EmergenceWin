namespace MattEland.Emergence.Domain

type IInteractive =
  abstract member interact: WorldObject -> GameMessage seq;

