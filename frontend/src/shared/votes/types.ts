export type Comments = {
  items: Comment[];
};

export type Comment = {
  user_login: string;
  text: string;
  delta: number;
  created_at: string;
};
