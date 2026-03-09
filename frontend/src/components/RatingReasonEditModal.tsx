import { RatingReason } from "@/shared/ratings/types";
import React from "react";

type Props = {
  item: RatingReason;
  onClose: () => void;
  onSave: (item: RatingReason) => void;
};

export default function RatingReasonEditModal({
  item,
  onClose,
  onSave,
}: Props) {
  const [code, setCode] = React.useState<string>(item.code);
  const [value, setValue] = React.useState<number>(item.value);
  const [isActive, setIsActive] = React.useState<boolean>(item.is_active);

  return (
    <div className="fixed inset-0 flex h-full w-full items-center justify-center bg-black/40">
      <div className="flex w-72 flex-col justify-between rounded bg-white">
        <div className="flex flex-col gap-3 p-3">
          {item.id && (
            <label className="block w-full font-mono text-sm">
              ID: {item.id}
            </label>
          )}
          <input
            type="text"
            className="w-full rounded border p-2"
            value={code}
            onChange={(x) => setCode(x.target.value)}
          />

          <input
            type="number"
            step="1"
            className="w-full rounded border p-2"
            value={value}
            onChange={(x) => setValue(Number(x.target.value))}
          />

          <label className="flex cursor-pointer items-center justify-between gap-2 text-sm">
            <span>Active</span>
            <input
              type="checkbox"
              className="h-6 w-6 cursor-pointer"
              checked={isActive}
              onChange={(x) => setIsActive(x.target.checked)}
            />
          </label>
        </div>
        <div className="flex justify-end gap-3 p-3">
          <button
            className="w-20 cursor-pointer rounded border bg-red-200 p-2"
            onClick={onClose}
          >
            Cancel
          </button>
          <button
            className="w-20 cursor-pointer rounded border bg-green-200 p-2"
            onClick={() =>
              onSave({
                id: item.id,
                code: code,
                value: value,
                is_active: isActive,
              })
            }
          >
            Save
          </button>
        </div>
      </div>
    </div>
  );
}
