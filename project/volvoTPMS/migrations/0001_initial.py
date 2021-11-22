# Generated by Django 3.2.8 on 2021-11-19 14:02

from django.db import migrations, models
import django.db.models.deletion


class Migration(migrations.Migration):

    initial = True

    dependencies = [
    ]

    operations = [
        migrations.CreateModel(
            name='Sensor',
            fields=[
                ('id', models.BigAutoField(auto_created=True, primary_key=True, serialize=False, verbose_name='ID')),
                ('temperature', models.IntegerField()),
                ('pressure', models.IntegerField()),
                ('remaning_battery', models.IntegerField()),
            ],
        ),
        migrations.CreateModel(
            name='Tire',
            fields=[
                ('id', models.BigAutoField(auto_created=True, primary_key=True, serialize=False, verbose_name='ID')),
                ('remaining_life', models.IntegerField()),
                ('baseline_pressure', models.IntegerField()),
                ('sensor', models.ForeignKey(on_delete=django.db.models.deletion.CASCADE, to='volvoTPMS.sensor')),
            ],
        ),
        migrations.CreateModel(
            name='Vehicle',
            fields=[
                ('id', models.BigAutoField(auto_created=True, primary_key=True, serialize=False, verbose_name='ID')),
                ('distance_driven', models.IntegerField()),
                ('machine_hours', models.IntegerField()),
                ('tires', models.ManyToManyField(to='volvoTPMS.Tire')),
            ],
        ),
    ]