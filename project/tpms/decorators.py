from django.http import HttpResponse
from django.http.response import Http404
from django.shortcuts import redirect

# TODO throw a 404 error if someone try to enter a page when is not authorize, for security reason 

# use in a NOT AUTH page ex: index page
# TODO redirect the admin to admin-page
def unauthenticated_user(view_func):
	def wrapper_func(request, *args, **kwargs):

		if request.user.is_authenticated:
			if request.user.groups.exists() and request.user.groups.all()[0].name == 'fleet-manager':
				return redirect('home')
			
			elif request.user.groups.exists() and request.user.groups.all()[0].name == 'company-administrator':
				return redirect('admin-page')
			
			else:
				return HttpResponse('You are logged in but you are in no group, the superadmin have to add you to one group. To login again and resolve the problem click <a href="/logout"> HERE </a>')		
		
		else:
			return view_func(request, *args, **kwargs)

	return wrapper_func

# only the admin can see this page
def company_administrator_only(view_func):
	def wrapper_function(request, *args, **kwargs):
		group = None
		if request.user.groups.exists():
			group = request.user.groups.all()[0].name
		if group == 'company-administrator':
			return view_func(request, *args, **kwargs)
		else:
			raise Http404
	return wrapper_function

# only the fleetmanager can see this page
def fleet_manager_only(view_func):
	def wrapper_function(request, *args, **kwargs):
		group = None
		if request.user.groups.exists():
			group = request.user.groups.all()[0].name
		if group == 'fleet-manager':
			return view_func(request, *args, **kwargs)
		else:
			raise Http404
	return wrapper_function