# Generated by Django 3.2.8 on 2021-11-22 13:55

from django.conf import settings
from django.db import migrations, models


class Migration(migrations.Migration):

    dependencies = [
        migrations.swappable_dependency(settings.AUTH_USER_MODEL),
        ('volvoTPMS', '0002_auto_20211119_1521'),
    ]

    operations = [
        migrations.AlterField(
            model_name='sensor',
            name='id',
            field=models.CharField(max_length=50, primary_key=True, serialize=False),
        ),
        migrations.AlterField(
            model_name='tire',
            name='id',
            field=models.CharField(max_length=50, primary_key=True, serialize=False),
        ),
        migrations.AlterField(
            model_name='vehicle',
            name='id',
            field=models.CharField(max_length=50, primary_key=True, serialize=False),
        ),
        migrations.AlterField(
            model_name='vehicle',
            name='tires',
            field=models.ManyToManyField(blank=True, to='volvoTPMS.Tire'),
        ),
        migrations.CreateModel(
            name='Company',
            fields=[
                ('id', models.BigAutoField(auto_created=True, primary_key=True, serialize=False, verbose_name='ID')),
                ('name', models.CharField(blank=True, max_length=50, null=True)),
                ('description', models.TextField(blank=True, null=True)),
                ('fleet_managers', models.ManyToManyField(blank=True, to=settings.AUTH_USER_MODEL)),
                ('vehicles', models.ManyToManyField(blank=True, to='volvoTPMS.Vehicle')),
            ],
        ),
    ]