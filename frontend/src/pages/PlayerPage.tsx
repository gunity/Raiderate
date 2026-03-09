"use client";

import { Player } from "@/shared/players/types";
import { Comment } from "@/shared/votes/types";
import SignedNumber from "@/shared/ui/number";
import { ReactNode } from "react";
import getLocalDateTime from "@/shared/common/datetime";

function Label({ children }: { children: ReactNode }) {
  return <div className="text-xs text-gray-500">{children}</div>;
}

function CommentItem({ comment }: { comment: Comment }) {
  return (
    <div className="group">
      <div className="flex w-full justify-between">
        <div>
          <span className="font-mono font-semibold">{comment.user_login}</span>
        </div>
        <div className="flex gap-2">
          <span className="text-gray-500 opacity-0 transition-opacity duration-200 group-hover:opacity-100">
            {getLocalDateTime(comment.created_at)}
          </span>
          <SignedNumber value={comment.delta} className="font-mono font-bold" />
        </div>
      </div>
      <div className="flex">
        <span>{comment.text}</span>
      </div>
    </div>
  );
}

type Props = {
  player: Player;
  comments: Comment[];
};

export default function PlayerPage({ player, comments }: Props) {
  return (
    <div className="mx-auto grid w-full max-w-md gap-3 p-6">
      <div>
        <Label>nickname</Label>
        <div className="font-mono text-2xl font-semibold">
          {player.nickname}
        </div>
      </div>

      <div className="flex items-center justify-between">
        <div>
          <Label>reputation</Label>
          <SignedNumber
            value={player?.rating ?? 0}
            className="font-mono text-2xl font-bold"
          />
        </div>

        <div className="text-right">
          <Label>votes</Label>
          <div className="font-mono text-2xl font-bold">
            {player.votes_count}
          </div>
        </div>
      </div>

      <div>
        <Label>last comments</Label>
        {comments.length > 0 ? (
          comments.map((comment: Comment) => (
            <CommentItem
              key={`${comment.user_login}-${comment.created_at}`}
              comment={comment}
            />
          ))
        ) : (
          <div>No comments yet</div>
        )}
      </div>
    </div>
  );
}
