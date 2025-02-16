import { STATUS_SCHEMA } from '@/entities/Status';
import { z } from 'zod';
import { baseEntity } from './BaseEntity';

export const DEGREE_SCHEMA = baseEntity(
  z.object({
    userId: z.string(),
    name: z.string(),
    status: STATUS_SCHEMA,
  }),
);

export type Degree = z.infer<typeof DEGREE_SCHEMA>;
