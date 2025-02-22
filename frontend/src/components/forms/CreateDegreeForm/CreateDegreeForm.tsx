'use client';

import { createDegree } from '@/app/actions/degrees';
import { Button, Input, Select } from '@/components';
import { STATUS_MAP, STATUS_SCHEMA, STATUS_TEXT_MAP } from '@/entities';
import { zodResolver } from '@hookform/resolvers/zod';
import clsx from 'clsx';
import { useRouter } from 'next/navigation';
import { FC, useState } from 'react';
import { SubmitHandler, useForm } from 'react-hook-form';
import { z } from 'zod';

const CREATE_DEGREE_SCHEMA = z.object({
  name: z.string().nonempty(),
  status: STATUS_SCHEMA.optional(),
});

type CreateDegreeSchema = z.infer<typeof CREATE_DEGREE_SCHEMA>;

export const CreateDegreeForm: FC = () => {
  const {
    handleSubmit,
    register,
    formState: { errors },
  } = useForm<CreateDegreeSchema>({
    resolver: zodResolver(CREATE_DEGREE_SCHEMA),
  });
  const router = useRouter();
  const [formError, setFormError] = useState<string | null>(null);
  const [isLoading, setIsLoading] = useState(false);

  const processForm: SubmitHandler<CreateDegreeSchema> = async (data) => {
    setIsLoading(true);
    setFormError(null);

    const response = await createDegree({
      name: data.name,
      status: data.status,
    });

    if (response.success) return router.push('/dashboard/degrees');

    setFormError(response.error);
    setIsLoading(false);
  };

  return (
    <form
      onSubmit={handleSubmit(processForm)}
      className={clsx(['flex', 'flex-col', 'w-full', 'gap-2.5'])}
    >
      <Input
        register={register('name')}
        label="Name"
        errors={errors}
        placeholder='e.g. "Bachelor of Science in Computer Science"'
        fullWidth
        required
      />

      <Select register={register('status')}
        label="Status"
        errors={errors}
        fullWidth
        required
      >
        {Object.values(STATUS_MAP).map((value) => {
          const text = STATUS_TEXT_MAP[value];

          return (
            <option key={value} value={value}>
              {text}
            </option>
          );
        })}
      </Select>

      {!!formError && (
        <p className={clsx('text-red-500', 'text-center')}>{formError}</p>
      )}

      <Button type="submit" loading={isLoading}>
        Create Degree
      </Button>
    </form>
  );
};
