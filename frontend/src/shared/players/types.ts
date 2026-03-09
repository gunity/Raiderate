export type Player = {
  id: number;
  nickname: string;
  rating: number;
  votes_count: number;
};

export type LeaderboardType = "Top" | "Bottom";

export type Leaderboard = {
  items: LeaderboardRow[];
};

export type LeaderboardRow = {
  position: number;
  nickname: string;
  rating: number;
};
