from django.db.models.signals import post_save
from django.contrib.auth.models import User
from django.contrib.auth.models import Group

from .models import CompanyAdministrator, FleetManager

def fleet_manager_profile(sender, instance, created, **kwargs):
	if created:
		group = Group.objects.get(name='fleet-manager')
		instance.groups.add(group)
		FleetManager.objects.create(
			user=instance,
			first_name=instance.first_name,
            last_name=instance.last_name,
            email=instance.email,
			)
		print('Profile created for the Fleet Manager %s!'%instance.username)
	
	if not created:
		fm = FleetManager.objects.get(user=instance)
		fm.first_name = instance.first_name
		fm.last_name = instance.last_name
		fm.email = instance.email
		fm.save()
		print('Profile update for the Fleet Manager %s!'%instance.username)

post_save.connect(fleet_manager_profile, sender=User)