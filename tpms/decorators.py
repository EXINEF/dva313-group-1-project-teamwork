from django.http import HttpResponse
from django.shortcuts import redirect

# TODO throw a 404 error if someone try to enter a page when is not authorize, for security reason 

# use in a NOT AUTH page ex: index page
# TODO redirect the admin to admin-page
def unauthenticated_user(view_func):
	def wrapper_func(request, *args, **kwargs):
		if request.user.is_authenticated:
			return redirect('home')
		else:
			return view_func(request, *args, **kwargs)

	return wrapper_func

# allows only the selected users to see the page
def allowed_users(allowed_roles=[]):
	def decorator(view_func):
		def wrapper_func(request, *args, **kwargs):

			group = None
			if request.user.groups.exists():
				group = request.user.groups.all()[0].name

			if group in allowed_roles:
				return view_func(request, *args, **kwargs)
			else:
				return HttpResponse('You are not authorized to view this page')
		return wrapper_func
	return decorator

# only the admin can see this page
def company_administrator_only(view_func):
	def wrapper_function(request, *args, **kwargs):
		group = None
		if request.user.groups.exists():
			group = request.user.groups.all()[0].name

		if group == 'fleet-manager':
			return redirect('home')

		if group == 'company-administrator':
			return view_func(request, *args, **kwargs)
		
		else:
			return HttpResponse('You are not authorized to view this page')

	return wrapper_function