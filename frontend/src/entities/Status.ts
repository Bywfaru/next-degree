import { z } from 'zod';

export const STATUS_MAP = {
  '0': 'not-started',
  '1': 'in-progress',
  '2': 'completed',
  '3': 'abandoned',
} as const;

export const STATUSES = [
  STATUS_MAP['0'],
  STATUS_MAP['1'],
  STATUS_MAP['2'],
  STATUS_MAP['3'],
] as const;

export const STATUS_SCHEMA = z.enum(STATUSES);

export type Status = z.infer<typeof STATUS_SCHEMA>;

export const STATUS_TEXT_MAP: Record<Status, string> = {
  'not-started': 'Not Started',
  'in-progress': 'In Progress',
  completed: 'Completed',
  abandoned: 'Abandoned',
};
