"use client";

import { useState } from "react";
import RatingReasonEditModal from "@/components/RatingReasonEditModal";
import { RatingReason } from "@/shared/ratings/types";
import { createRatingReason, updateRatingReason } from "@/shared/ratings/api";

type Props = {
  reasons: RatingReason[];
};

export default function AdminRatingReasonsPage({ reasons }: Props) {
  const [items, setItems] = useState<RatingReason[]>(reasons);
  const [error, setError] = useState<string | null>(null);

  const [editMode, setEditMode] = useState<boolean>(false);
  const [editValue, setEditValue] = useState<RatingReason | null>(null);

  function openModal(item: RatingReason) {
    setEditValue(item);
    setEditMode(true);
  }

  function closeModal() {
    setEditValue(null);
    setEditMode(false);
  }

  async function saveModal(item: RatingReason): Promise<void> {
    try {
      if (item.id === undefined) {
        const id = await createRatingReason(item);
        const created: RatingReason = { ...item, id };

        setItems((prev) => [created, ...prev]);
      } else {
        await updateRatingReason(item);

        setItems((prev) => prev.map((x) => (x.id === item.id ? item : x)));
      }

      closeModal();
    } catch (error: unknown) {
      setError(error instanceof Error ? error.message : "Save failed");
    }
  }

  return (
    <div className="m-2 grid gap-3">
      {items.map((item) => (
        <div
          key={item.id}
          className="flex w-100 items-center justify-between rounded border p-2"
        >
          <div>
            <div className="font-mono text-sm">{item.code}</div>
            <div className="text-xs text-gray-500">id: {item.id}</div>
          </div>

          <div className="flex items-center gap-4">
            <div
              className={[
                "text-sm font-semibold",
                item.value > 0 ? "text-green-700" : "",
                item.value < 0 ? "text-red-700" : "",
                item.value === 0 ? "text-gray-700" : "",
              ].join(" ")}
            >
              {item.value > 0 ? `+${item.value}` : item.value}
            </div>

            <span
              className={[
                "w-20 rounded border p-2 text-center text-xs",
                item.is_active
                  ? "border-green-200 bg-green-50 text-green-800"
                  : "border-gray-200 bg-gray-50 text-gray-700",
              ].join(" ")}
            >
              {item.is_active ? "active" : "disabled"}
            </span>

            <button
              className="w-16 cursor-pointer rounded border p-2 text-xs"
              onClick={() => openModal(item)}
            >
              Edit
            </button>
          </div>
        </div>
      ))}

      <button
        className="w-16 cursor-pointer rounded border p-2"
        onClick={() => openModal({ code: "", value: 0, is_active: true })}
      >
        Create
      </button>

      {editMode && editValue && (
        <RatingReasonEditModal
          item={editValue}
          onClose={closeModal}
          onSave={saveModal}
        />
      )}
    </div>
  );
}
