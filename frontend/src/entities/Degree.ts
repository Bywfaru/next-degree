import { z } from 'zod';
import { BASE_ENTITY_SCHEMA } from './BaseEntity';

export const DEGREE_SCHEMA = BASE_ENTITY_SCHEMA.and(z.object({
  userId: z.string(),
  name: z.string(),
  status: z.number().int(),
}));

export type Degree = z.infer<typeof DEGREE_SCHEMA>;
