import { STATUS_SCHEMA } from '@/entities/Status';
import { z } from 'zod';

export const DEGREE_SCHEMA = z.object({
  userId: z.string(),
  name: z.string(),
  status: STATUS_SCHEMA,
  id: z.number(),
  createdAt: z.coerce.date(),
  updatedAt: z.coerce.date(),
});

export type Degree = z.infer<typeof DEGREE_SCHEMA>