export type VoteComments = {
    items: VoteComment[];
}

export type VoteComment = {
    user_login: string;
    text: string;
    delta: number;
    created_at: string;
};